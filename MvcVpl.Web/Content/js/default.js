; (function ($) {
    $(function () {
        var syntaxNumber = 0;
        var blockNumber = 0;
        var models = {
            Position: {},
            New: {},
            Ast: {},
            JavaScript: "",
            CSharp:""
        };
        var Syntax = function (syntax) {
            if (!syntax)
                return {};
            this.Id = "syn" + (++syntaxNumber);
            this.Kind = syntax.Kind;
            this.Type = syntax.Type;
            this.Name = syntax.Name;
            this.Value = syntax.Value;
            if (syntax.Primary) {
                this.Primary = syntax.Primary instanceof Syntax ? syntax.Primary : new Syntax(syntax.Primary);
            }
            if (syntax.Secondary && syntax.OpCode && syntax.OpCode !== "") {
                this.OpCode = syntax.OpCode;
                this.Secondary = syntax.Secondary instanceof Syntax ? syntax.Secondary : new Syntax(syntax.Secondary);
            }
            if (syntax.Block && syntax.Block.hasOwnProperty("SyntaxTree"))
                this.Block = new Block(syntax.Block);

            this.ViewerValue = function (syntax) {
                if (syntax.Type === "string")
                    return "\"" + syntax.Value + "\"";

                return syntax.Value;
            }
            this.ViewerOperation = function (syntax) {
                var ops = {
                    "": "",
                    "add": "+",
                    "sub": "-",
                    "mul": "*",
                    "div": "/",
                    "eq": "==",
                    "lt": "<",
                    "lte": "<=",
                    "gt": ">",
                    "gte": ">="
                };

                return ops[syntax.OpCode] || "unknown";
            };

            this.ViewerTemplateName = function (syntax) {
                var viewers = {
                    "constant": "constant-viewer",
                    "var": "var-viewer",
                    "ref": "ref-viewer",
                    "assign": "assign-viewer",
                    "operation": "operation-viewer",
                    "if": "if-viewer",
                    "while": "while-viewer",
                    "return": "return-viewer",
                    "break": "break-viewer",
                    "continue": "continue-viewer",
                };

                return viewers[syntax.Kind] || "default-viewer";
            };
        }

        var Block = function (block) {
            if (!block)
                return {};

            var map = $.map(block.SyntaxTree, function (syntax) { return new Syntax(syntax); });
            var self = this;

            this.Id = "blk" + (++blockNumber);
            this.SyntaxTree = ko.observableArray(map);
            this.SelectAddPosition = function (syntax) {
                models.Position.SyntaxTree = syntax instanceof Block ? syntax.SyntaxTree : self.SyntaxTree;
                models.Position.Syntax = syntax instanceof Syntax ? syntax : null;

                return true;
            };
            this.DeleteSyntax = function (syntax) {
                self.SyntaxTree.remove(syntax);

                astRebuilded();
            };
        };


        var astRebuilded = function () {
            var data = ko.toJSON({ block: models.Ast });

            console.log(data);

            var js = {
                type: "post",
                url: "/Generate/JavaScript",
                data: data,
                contentType: "application/json",
            };
            var cs = {
                type: "post",
                url: "/Generate/CSharp",
                data: data,
                contentType: "application/json",
            };

            $.when(
                $.ajax(js),
                $.ajax(cs)
            ).done(function (jsResult, csResult) {
                var javascriptSource = jsResult[0];
                var csharpSource = csResult[0];
                // JavaScript Generate & Execute
                $("#javascript-preview").removeClass("prettyprinted").text(javascriptSource);
                $("#javascript-result").removeClass("prettyprinted");
                models.JavaScript = javascriptSource;


                // CSharp Generate & Execute
                $("#csharp-preview").removeClass("prettyprinted").text(csharpSource);
                $("#csharp-result").removeClass("prettyprinted").text("");
                models.CSharp = csharpSource;

                prettyPrint();
            });
        };

        $("#run").on("click",function() {
            $("#javascriptSource").val(models.JavaScript);
            $("#javascriptForm").submit();

            $("#csharpSource").val(models.CSharp);
            $("#csharpForm").submit();
        });

        $("#select-ast").on("click", function () {
            var selected = $("#sample-ast").val();
            models.Ast.SyntaxTree.removeAll();
            if (selected !== "new") {
                var selected = samples[selected];
                var map = $.map(selected.SyntaxTree, function (syntax) { return new Syntax(syntax); });
                map.forEach(function (syntax) { models.Ast.SyntaxTree.push(syntax) });
            }
            astRebuilded();
        });

        var expressionBuilder = function (kind, value) {
            var expression = {};
            if (kind === "var") {
                expression.Kind = "var";
                expression.Name = value;
            } else if (kind === "ref") {
                expression.Kind = "ref";
                expression.Name = value;
            } else {
                expression.Kind = "constant";
                expression.Type = kind;
                expression.Value = value;
            }

            return expression;
        };

        $("#add-syntax").on("click", function () {
            var operation = models.New.ExpOperation();
            var syntax = {
                Kind: models.New.Kind()
            };

            // Type
            if (syntax.Kind === "var") {
                syntax.Type = models.New.VarType();
            }

            // Name
            if (syntax.Kind === "var"
                || syntax.Kind === "ref"
                || syntax.Kind === "assign") {
                syntax.Name = models.New.VarName();
            }

            // Expression
            if (syntax.Kind === "assign"
                || syntax.Kind === "if"
                || syntax.Kind === "while"
                || syntax.Kind === "return") {

                if (operation === "") {
                    // values
                    syntax.Primary = expressionBuilder(models.New.ExpPrimaryKind(), models.New.ExpPrimaryValue());
                } else {
                    // operation
                    syntax.Primary = new Syntax({
                        Kind: "operation",
                        OpCode: operation,
                        Primary: expressionBuilder(models.New.ExpPrimaryKind(), models.New.ExpPrimaryValue()),
                        Secondary: expressionBuilder(models.New.ExpSecondaryKind(), models.New.ExpSecondaryValue())
                    });
                }
            }

            // Inner block
            if (syntax.Kind === "if"
                || syntax.Kind === "while") {
                syntax.Block = { SyntaxTree: [] };
            }

            if (models.Position.SyntaxTree) {
                var add = new Syntax(syntax);
                if (models.Position.Syntax == null) {
                    models.Position.SyntaxTree.push(add);
                } else {
                    var index = models.Position.SyntaxTree.indexOf(models.Position.Syntax);
                    models.Position.SyntaxTree.splice(index, 0, add);
                }
            }

            astRebuilded();
        });

        // AST viewer
        models.Ast = new Block({SyntaxTree:[]});
        ko.applyBindings(models.Ast, document.getElementById("viewer"));

        // AST editor
        models.Position = { SyntaxTree: models.Ast.SyntaxTree, Syntax: null };
        models.New = {
            Kind: ko.observable("var"),
            VarType: ko.observable(),
            VarName: ko.observable(),
            ExpPrimaryKind: ko.observable(),
            ExpPrimaryValue: ko.observable(),
            ExpSecondaryKind: ko.observable(),
            ExpSecondaryValue: ko.observable(),
            ExpOperation: ko.observable(),

            showVar: function () {
                return this.Kind() === "var";
            },
            showName: function () {
                return this.Kind() === "var" || this.Kind() === "assign";
            },
            hasExpressionKind: function () {
                return this.Kind() === "assign" || this.Kind() === "if" || this.Kind() === "while";
            },
            showPrimary: function () {
                return this.hasExpressionKind() || this.Kind() === "return";
            },
            showSecondary: function () {
                return this.hasExpressionKind();
            },
            showOperation: function () {
                return this.hasExpressionKind();
            }
        };

        ko.applyBindings(models.New, document.getElementById("adder"));

        // Initialize
        $("input[name='add-position']").last().attr("checked", true);
        $("select").material_select();
        astRebuilded();
    });
}(jQuery));
