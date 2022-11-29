using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magma
{
    internal class Globals
    {
        public static bool IsUserTyping = false;
        public static string SearchString = "";
        public static Executor ExecutorPage = new Executor();
        public static string AllowedChars = "abcdefghijklmnopqrstuvwxyz";

        public static List<CompletionItem> Strings = new List<CompletionItem>();
        public static List<CompletionItem> Properties = new List<CompletionItem>
        {
            new CompletionItem()
            {
                Name = "Enum", Type = "property", Description = ""
            },
            new CompletionItem()
            {
                Name = "game", Type = "property", Description = ""
            },
            new CompletionItem()
            {
                Name = "shared", Type = "property", Description = ""
            },
            new CompletionItem()
            {
                Name = "script", Type = "property", Description = ""
            },
            new CompletionItem()
            {
                Name = "workspace", Type = "property", Description = ""
            }
        };
        public static List<CompletionItem> Functions = new List<CompletionItem>
        {
            new CompletionItem()
            {
                Name = "loadstring", Type = "function", Description = "(contents: string): Variant"
            },
            new CompletionItem()
            {
                Name = "assert", Type = "function", Description = "(value: Varient, errorMessage: string): Variant"
            },
            new CompletionItem()
            {
                Name = "collectgarbage", Type = "function", Description = "(operation: string): Variant"
            },
            new CompletionItem()
            {
                Name = "error", Type = "function", Description = "(message: string, level: number)"
            },
            new CompletionItem()
            {
                Name = "getfenv", Type = "function", Description = "(stack: Variant): table"
            },
            new CompletionItem()
            {
                Name = "getmetatable", Type = "function", Description = "(t: Variant): Variant"
            },
            new CompletionItem()
            {
                Name = "ipairs", Type = "function", Description = "(t: Array): function"
            },
            new CompletionItem()
            {
                Name = "newproxy", Type = "function", Description = "(addMetatable: boolean): userdata"
            },
            new CompletionItem()
            {
                Name = "next", Type = "function", Description = "(t: table, lastKey: Variant): Variant"
            },
            new CompletionItem()
            {
                Name = "pairs", Description = "(t: table): function", Type = "function"
            },
            new CompletionItem()
            {
                Name = "pcall", Description = "(func: function, args: Tuple): boolean", Type = "function"
            },
            new CompletionItem()
            {
                Name = "print", Description = "(params: Tuple)", Type = "function"
            },
            new CompletionItem()
            {
                Name = "rawequal", Description = "(v1: Variant, v2: Variant): boolean", Type = "function"
            },
            new CompletionItem()
            {
                Name = "rawget", Description = "(t: table, index: Variant): Variant", Type = "function"
            },
            new CompletionItem()
            {
                Name = "rawset", Description = "(t: table, index: Variant, value: Variant): table", Type = "function"
            },
            new CompletionItem()
            {
                Name = "select", Description = "(index: Variant, args: Tuple): Tuple", Type = "function"
            },
            new CompletionItem()
            {
                Name = "setfenv", Description = "(f: Variant, fenv: table): Variant", Type = "function"
            },
            new CompletionItem()
            {
                Name = "setmetatable", Description = "(t: table, newMeta: Variant): table", Type = "function"
            },
            new CompletionItem()
            {
                Name = "tonumber", Description = "(arg: Variant, base: number): Varient", Type = "function"
            },
            new CompletionItem()
            {
                Name = "tostring", Description = "(arg: Variant): string", Type = "function"
            },
            new CompletionItem()
            {
                Name = "type", Description = "(v: Variant): string", Type = "function"
            },
            new CompletionItem()
            {
                Name = "unpack", Description = "(list: table, i: number, j: number): Variant", Type = "function"
            },
            new CompletionItem()
            {
                Name = "xpcall", Description = "(f: function, err: function, args: Tuple): boolean", Type = "function"
            },
            new CompletionItem()
            {
                Name = "delay", Type = "function", Description = "(delayTime: number, callback: function)"
            },
            new CompletionItem()
            {
                Name = "elapsedTime", Type = "function", Description = "(): number"
            },
            new CompletionItem()
            {
                Name = "gcinfo", Type = "function", Description = "(): number"
            },
            new CompletionItem()
            {
                Name = "require", Type = "function", Description = "(module: ModuleScript): Variant"
            },
            new CompletionItem()
            {
                Name = "stats", Type = "function", Description = "(): Stats"
            },
            new CompletionItem()
            {
                Name = "tick", Type = "function", Description = "(): number"
            },
            new CompletionItem()
            {
                Name = "time", Type = "function", Description = "(): number"
            },
            new CompletionItem()
            {
                Name = "typeof", Type = "function", Description = "(object: Variant): string"
            },
            new CompletionItem()
            {
                Name = "UserSettings", Type = "function", Description = "()"
            },
            new CompletionItem()
            {
                Name = "version", Type = "function", Description = "(): string"
            },
            new CompletionItem()
            {
                Name = "wait", Type = "function", Description = "(seconds: number): number"
            },
            new CompletionItem()
            {
                Name = "warn", Type = "function", Description = "(params: Tuple)"
            }
        };
        public static List<CompletionItem> Logic = new List<CompletionItem> {
            new CompletionItem()
            {
                Name = "and", Type = "logic", Description = ""
            },
            new CompletionItem()
            {
                Name = "false", Type = "logic", Description = ""
            },
            new CompletionItem()
            {
                Name = "not", Type = "logic", Description = ""
            },
            new CompletionItem()
            {
                Name = "or", Type = "logic", Description = ""
            },
            new CompletionItem()
            {
                Name = "true", Type = "logic", Description = ""
            }
        };
        public static List<CompletionItem> Keywords = new List<CompletionItem>
        {
            new CompletionItem()
            {
                Name = "break", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "do", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "else", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "elseif", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "end", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "for", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "function", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "global", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "if", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "in", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "local", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "nil", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "repeat", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "return", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "then", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "until", Type = "keyword", Description = ""
            },
            new CompletionItem()
            {
                Name = "while", Type = "keyword", Description = ""
            }
        };

        public static List<CompletionItem> MasterCompletionList = new List<CompletionItem>();
    }

    public class CompletionItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
}
