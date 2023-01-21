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
        //public static Injector InjectorPage = new Injector();
        public static string AllowedChars = "abcdefghijklmnopqrstuvwxyz";

        public static List<string> TypePriority = new List<string>(){
            "special",
            "keyword",
            "function",
            "object",
            "type",
            "logic",
            "property"
        };

        // Literally a list of keywords for the autocomplete engine in the various categories
        public static List<CompletionItem> Strings = new List<CompletionItem>();
        public static List<CompletionItem> Special = new List<CompletionItem>
        {
            new CompletionItem()
            {
                Name = "game", Type = "special", Description = "Represents the game object"
            }
        };
        public static List<CompletionItem> Properties = new List<CompletionItem>
        {
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
        public static List<CompletionItem> Types = new List<CompletionItem>
        {
            new CompletionItem()
            {
                Name = "Axes", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "bit32", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "BrickColor", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "CFrame", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Color3", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "coroutine", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "ColorSequence", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "CatalogSearchParams", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "ColorSequenceKeypoint", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "DateTime", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "DockWidgetPluginGuiInfo", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Enum", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "File", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Font", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Faces", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "FloatCurveKey", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Instance", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "math", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "NumberRange", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "NumberSequence", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "NumberSequenceKeypoint", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "os", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "OverlapParams", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "PathWaypoint", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "PhysicalProperties", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Ray", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Rect", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Random", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Region3", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Region3int16", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "RayCastParams", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "RotationCurveKey", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "script", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "shared", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "string", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "self", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "task", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "table", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "TweenInfo", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "UDim", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "utf8", Type = "object", Description = ""
            },
            new CompletionItem()
            {
                Name = "UDim2", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Vector2", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Vector3", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Vector2int16", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "Vector3int16", Type = "type", Description = ""
            },
            new CompletionItem()
            {
                Name = "workspace", Type = "object", Description = ""
            }
        };
        public static List<CompletionItem> Functions = new List<CompletionItem>
        {
            new CompletionItem()
            {
                Name = "loadstring", Type = "function", Usage = "(contents: string): Variant", Description = "Returns the provided code as a function that can be executed"
            },
            new CompletionItem()
            {
                Name = "assert", Type = "function", Usage = "(value: Varient, errorMessage: string): Variant", Description = "Throws an error if the provided value resolves to false or nil"
            },
            new CompletionItem()
            {
                Name = "collectgarbage", Type = "function", Usage = "(operation: string): Variant"
            },
            new CompletionItem()
            {
                Name = "error", Type = "function", Usage = "(message: string, level: number)"
            },
            new CompletionItem()
            {
                Name = "getfenv", Type = "function", Usage = "(stack: Variant): table"
            },
            new CompletionItem()
            {
                Name = "getmetatable", Type = "function", Usage = "(t: Variant): Variant"
            },
            new CompletionItem()
            {
                Name = "ipairs", Type = "function", Usage = "(t: Array): function"
            },
            new CompletionItem()
            {
                Name = "newproxy", Type = "function", Usage = "(addMetatable: boolean): userdata"
            },
            new CompletionItem()
            {
                Name = "next", Type = "function", Usage = "(t: table, lastKey: Variant): Variant"
            },
            new CompletionItem()
            {
                Name = "pairs", Usage = "(t: table): function", Type = "function"
            },
            new CompletionItem()
            {
                Name = "pcall", Usage = "(func: function, args: Tuple): boolean", Type = "function"
            },
            new CompletionItem()
            {
                Name = "print", Usage = "(params: Tuple)", Type = "function", Description = "Prints all provided values to the output."
            },
            new CompletionItem()
            {
                Name = "rawequal", Usage = "(v1: Variant, v2: Variant): boolean", Type = "function"
            },
            new CompletionItem()
            {
                Name = "rawget", Usage = "(t: table, index: Variant): Variant", Type = "function"
            },
            new CompletionItem()
            {
                Name = "rawset", Usage = "(t: table, index: Variant, value: Variant): table", Type = "function"
            },
            new CompletionItem()
            {
                Name = "select", Usage = "(index: Variant, args: Tuple): Tuple", Type = "function"
            },
            new CompletionItem()
            {
                Name = "setfenv", Usage = "(f: Variant, fenv: table): Variant", Type = "function"
            },
            new CompletionItem()
            {
                Name = "setmetatable", Usage = "(t: table, newMeta: Variant): table", Type = "function"
            },
            new CompletionItem()
            {
                Name = "tonumber", Usage = "(arg: Variant, base: number): Varient", Type = "function"
            },
            new CompletionItem()
            {
                Name = "tostring", Usage = "(arg: Variant): string", Type = "function"
            },
            new CompletionItem()
            {
                Name = "type", Usage = "(v: Variant): string", Type = "function"
            },
            new CompletionItem()
            {
                Name = "unpack", Usage = "(list: table, i: number, j: number): Variant", Type = "function"
            },
            new CompletionItem()
            {
                Name = "xpcall", Usage = "(f: function, err: function, args: Tuple): boolean", Type = "function"
            },
            new CompletionItem()
            {
                Name = "delay", Type = "function", Usage = "(delayTime: number, callback: function)"
            },
            new CompletionItem()
            {
                Name = "elapsedTime", Type = "function", Usage = "(): number"
            },
            new CompletionItem()
            {
                Name = "gcinfo", Type = "function", Usage = "(): number"
            },
            new CompletionItem()
            {
                Name = "require", Type = "function", Usage = "(module: ModuleScript): Variant"
            },
            new CompletionItem()
            {
                Name = "stats", Type = "function", Usage = "(): Stats"
            },
            new CompletionItem()
            {
                Name = "tick", Type = "function", Usage = "(): number"
            },
            new CompletionItem()
            {
                Name = "time", Type = "function", Usage = "(): number"
            },
            new CompletionItem()
            {
                Name = "typeof", Type = "function", Usage = "(object: Variant): string"
            },
            new CompletionItem()
            {
                Name = "UserSettings", Type = "function", Usage = "()"
            },
            new CompletionItem()
            {
                Name = "version", Type = "function", Usage = "(): string"
            },
            new CompletionItem()
            {
                Name = "wait", Type = "function", Usage = "(seconds: number): number"
            },
            new CompletionItem()
            {
                Name = "warn", Type = "function", Usage = "(params: Tuple)"
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

        public string Usage { get; set; }

    }
}
