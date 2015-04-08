var samples = {
    "string": {
        "SyntaxTree": [
            {
                "Primary": {
                    "Value": "takepara",
                    "Type": "string",
                    "Kind": "constant"
                },
                "Kind": "return"
            }
        ]
    },
    "add": {
        "SyntaxTree": [
            {
                "Name": "result",
                "Type": "int",
                "Kind": "var"
            }, {
                "Primary": {
                    "Primary": {
                        "Value": "5",
                        "Type": "int",
                        "Kind": "constant"
                    },
                    "Secondary": {
                        "Value": "8",
                        "Type": "int",
                        "Kind": "constant"
                    },
                    "OpCode": "add",
                    "Kind": "operation"
                },
                "Name": "result",
                "Kind": "assign"
            },
            {
                "Primary": {
                    "Name": "result",
                    "Kind": "ref"
                },
                "Kind": "return"
            }
        ]
    },
    "loop": {
        "SyntaxTree": [
        {
            "Name": "result",
            "Type": "int",
            "Kind": "var"
        },
        {
            "Primary": {
                "Primary": {
                    "Value": "5",
                    "Type": "int",
                    "Kind": "constant"
                },
                "Secondary": {
                    "Value": "8",
                    "Type": "int",
                    "Kind": "constant"
                },
                "OpCode": "add",
                "Kind": "operation"
            },
            "Name": "result",
            "Kind": "assign"
        },
        {
            "Primary": {
                "Primary": {
                    "Name": "result",
                    "Kind": "ref"
                },
                "Secondary": {
                    "Value": "100",
                    "Type": "int",
                    "Kind": "constant"
                },
                "OpCode": "lt",
                "Kind": "operation"
            },
            "Block": {
                "SyntaxTree": [
                         {
                             "Primary": {
                                 "Primary": {
                                     "Name": "result",
                                     "Kind": "ref"
                                 },
                                 "Secondary": {
                                     "Value": "3",
                                     "Type": "int",
                                     "Kind": "constant"
                                 },
                                 "OpCode": "mul",
                                 "Kind": "operation"
                             },
                             "Name": "result",
                             "Kind": "assign"
                         }
                ]
            },
            "Kind": "while"
        }, {
            "Primary": {
                "Name": "result",
                "Kind": "ref"
            },
            "Kind": "return"
        }]
    },
    "kuku": {
        "SyntaxTree": [
           {
               "Kind": "var",
               "Type": "string",
               "Name": "kuku"
           },
           {
               "Kind": "assign",
               "Name": "kuku",
               "Primary": {
                   "Kind": "constant",
                   "Type": "string",
                   "Value": ""
               }
           },
           {
               "Kind": "var",
               "Type": "int",
               "Name": "i"
           },
           {
               "Kind": "var",
               "Type": "int",
               "Name": "j"
           },
           {
               "Kind": "assign",
               "Name": "i",
               "Primary": {
                   "Kind": "constant",
                   "Type": "int",
                   "Value": "1"
               }
           },
           {
               "Kind": "while",
               "Primary": {
                   "Kind": "operation",
                   "Primary": {
                       "Kind": "ref",
                       "Name": "i"
                   },
                   "OpCode": "lt",
                   "Secondary": {
                       "Kind": "constant",
                       "Type": "int",
                       "Value": "10"
                   }
               },
               "Block": {
                   "SyntaxTree": [
                      {
                          "Kind": "assign",
                          "Name": "kuku",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": " kuku"
                              },
                              "OpCode": "add",
                              "Secondary": {
                                  "Kind": "ref",
                                  "Name": "i "
                              }
                          }
                      },
                      {
                          "Kind": "assign",
                          "Name": "kuku",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": " kuku"
                              },
                              "OpCode": "add",
                              "Secondary": {
                                  "Kind": "constant",
                                  "Type": "string",
                                  "Value": " の段 "
                              }
                          }
                      },
                      {
                          "Kind": "assign",
                          "Name": "j",
                          "Primary": {
                              "Kind": "constant",
                              "Type": "int",
                              "Value": " 1"
                          }
                      },
                      {
                          "Kind": "while",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": "j"
                              },
                              "OpCode": "lt",
                              "Secondary": {
                                  "Kind": "constant",
                                  "Type": "int",
                                  "Value": "10"
                              }
                          },
                          "Block": {
                              "SyntaxTree": [
                                 {
                                     "Kind": "var",
                                     "Type": "int",
                                     "Name": "mul"
                                 },
                                 {
                                     "Kind": "assign",
                                     "Name": "mul",
                                     "Primary": {
                                         "Kind": "operation",
                                         "Primary": {
                                             "Kind": "ref",
                                             "Name": "i"
                                         },
                                         "OpCode": "mul",
                                         "Secondary": {
                                             "Kind": "ref",
                                             "Name": "j"
                                         }
                                     }
                                 },
                                 {
                                     "Kind": "assign",
                                     "Name": "kuku",
                                     "Primary": {
                                         "Kind": "operation",
                                         "Primary": {
                                             "Kind": "ref",
                                             "Name": " kuku"
                                         },
                                         "OpCode": "add",
                                         "Secondary": {
                                             "Kind": "ref",
                                             "Name": "mul"
                                         }
                                     }
                                 },
                                 {
                                     "Kind": "assign",
                                     "Name": "kuku",
                                     "Primary": {
                                         "Kind": "operation",
                                         "Primary": {
                                             "Kind": "ref",
                                             "Name": " kuku"
                                         },
                                         "OpCode": "add",
                                         "Secondary": {
                                             "Kind": "constant",
                                             "Type": "string",
                                             "Value": " "
                                         }
                                     }
                                 },
                                 {
                                     "Kind": "assign",
                                     "Name": "j",
                                     "Primary": {
                                         "Kind": "operation",
                                         "Primary": {
                                             "Kind": "ref",
                                             "Name": "j"
                                         },
                                         "OpCode": "add",
                                         "Secondary": {
                                             "Kind": "constant",
                                             "Type": "int",
                                             "Value": "1"
                                         }
                                     }
                                 }
                              ]
                          }
                      },
                      {
                          "Kind": "assign",
                          "Name": "kuku",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": " kuku"
                              },
                              "OpCode": "add",
                              "Secondary": {
                                  "Kind": "constant",
                                  "Type": "string",
                                  "Value": " \\r\\n"
                              }
                          }
                      },
                      {
                          "Kind": "assign",
                          "Name": "i",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": "i"
                              },
                              "OpCode": "add",
                              "Secondary": {
                                  "Kind": "constant",
                                  "Type": "int",
                                  "Value": "1"
                              }
                          }
                      }
                   ]
               }
           },
           {
               "Kind": "return",
               "Primary": {
                   "Kind": "ref",
                   "Name": " kuku"
               }
           }
        ]
    },
    "sort": {
        "SyntaxTree": [
           {
               "Kind": "var",
               "Type": "string",
               "Name": "original"
           },
           {
               "Kind": "var",
               "Type": "int",
               "Name": "len"
           },
           {
               "Kind": "assign",
               "Name": "original",
               "Primary": {
                   "Kind": "constant",
                   "Type": "string",
                   "Value": "takepara"
               }
           },
           {
               "Kind": "assign",
               "Name": "len",
               "Primary": {
                   "Kind": "ref",
                   "Name": "Length(original)"
               }
           },
           {
               "Kind": "var",
               "Type": "int",
               "Name": "i"
           },
           {
               "Kind": "var",
               "Type": "int",
               "Name": "j"
           },
           {
               "Kind": "assign",
               "Name": "i",
               "Primary": {
                   "Kind": "constant",
                   "Type": "int",
                   "Value": "0"
               }
           },
           {
               "Kind": "while",
               "Primary": {
                   "Kind": "operation",
                   "Primary": {
                       "Kind": "ref",
                       "Name": "i"
                   },
                   "OpCode": "lt",
                   "Secondary": {
                       "Kind": "ref",
                       "Name": "len"
                   }
               },
               "Block": {
                   "SyntaxTree": [
                      {
                          "Kind": "assign",
                          "Name": "j",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": "i"
                              },
                              "OpCode": "add",
                              "Secondary": {
                                  "Kind": "constant",
                                  "Type": "int",
                                  "Value": "1"
                              }
                          }
                      },
                      {
                          "Kind": "while",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": "j"
                              },
                              "OpCode": "lt",
                              "Secondary": {
                                  "Kind": "ref",
                                  "Name": "len"
                              }
                          },
                          "Block": {
                              "SyntaxTree": [
                                 {
                                     "Kind": "if",
                                     "Primary": {
                                         "Kind": "operation",
                                         "Primary": {
                                             "Kind": "ref",
                                             "Name": "original[i]"
                                         },
                                         "OpCode": "gt",
                                         "Secondary": {
                                             "Kind": "ref",
                                             "Name": "original[j]"
                                         }
                                     },
                                     "Block": {
                                         "SyntaxTree": [
                                            {
                                                "Kind": "assign",
                                                "Name": "original",
                                                "Primary": {
                                                    "Kind": "ref",
                                                    "Name": "CharExchange(original,i,j)"
                                                }
                                            }
                                         ]
                                     }
                                 },
                                 {
                                     "Kind": "assign",
                                     "Name": "j",
                                     "Primary": {
                                         "Kind": "operation",
                                         "Primary": {
                                             "Kind": "ref",
                                             "Name": "j"
                                         },
                                         "OpCode": "add",
                                         "Secondary": {
                                             "Kind": "constant",
                                             "Type": "int",
                                             "Value": "1"
                                         }
                                     }
                                 }
                              ]
                          }
                      },
                      {
                          "Kind": "assign",
                          "Name": "i",
                          "Primary": {
                              "Kind": "operation",
                              "Primary": {
                                  "Kind": "ref",
                                  "Name": "i"
                              },
                              "OpCode": "add",
                              "Secondary": {
                                  "Kind": "constant",
                                  "Type": "int",
                                  "Value": "1"
                              }
                          }
                      }
                   ]
               }
           },
           {
               "Kind": "return",
               "Primary": {
                   "Kind": "ref",
                   "Name": "original"
               }
           }
        ]
    }
};