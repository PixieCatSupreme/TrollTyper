using NLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrollTyper
{
    class TrollTyper
    {
        private const string textCommand = "-t";
        private const string clipBoardCommand = "-c";
        private const string helpCommand = "-h";
        private const string bbcCommand = "-b";
        private const string reloadCommand = "-r";
        private const string clearCommand = "-clear";
        private const string quitCommand = "-q";

        private readonly string _outputPath;
        private readonly string _quirkPath;
        private readonly string _luaSandbox;

        private Converter _converter;

        private bool _isTextMode;
        private bool _isClipBoardMode;
        private bool _isBbcMode;
        private bool _quit;
        private bool _ranCommand;

        private List<string> _args;
        private string _currentFileName;

        public TrollTyper(string[] args)
        {
            _args = args.ToList();
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _outputPath = $@"{currentPath}\Output\";
            _quirkPath = $@"{currentPath}\Quirks\";
            _currentFileName = "";

            _isTextMode = false;
            _isClipBoardMode = false;
            _isBbcMode = true;
            _quit = false;
            _ranCommand = false;

            _luaSandbox = "import ('TrollTyper', 'TrollTyper.Scripting') \n import = function () end";

            _converter = new Converter();
        }

        public bool LoadLua()
        {
            _converter.TypingQuirks.Clear();
            List<TypingQuirk> quirks = new List<TypingQuirk>();

            string[] fileNames = Directory.GetFiles(_quirkPath, "*.lua", System.IO.SearchOption.AllDirectories);

            string currentPath = "";
            for (int i = 0; i < fileNames.Length; i++)
            {
                try
                {
                    currentPath = fileNames[i];

                    Lua lua = new Lua();
                    lua.LoadCLRPackage();
                    lua.DoString(_luaSandbox);
                    lua.DoFile(currentPath);
                    quirks.Add(new TypingQuirk(lua));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine($"Loaded {quirks.Count} quirk script files.");
            _converter.TypingQuirks = quirks;
            return true;
        }

        public bool Run()
        {
            WriteStartText();
            LoadLua();

            if (_args.Count > 0 && !_args.Contains(helpCommand))
            {
                _quit = true;
                return ReadArguments();
            }
            else
            {
                return RepeatMode();
            }
        }

        private void WriteStartText()
        {
            Console.WriteLine("Welcome to Trolltyper! The application to change your text into Homestuck style logs.");
        }

        private bool RepeatMode()
        {
            while (!_quit)
            {
                _isTextMode = false;
                _isClipBoardMode = false;
                _isBbcMode = true;

                Console.Write("\nPlease put in your input: ");
                SplitArguments(Console.ReadLine());
                ReadArguments();
            }

            return true;
        }

        private void SplitArguments(string input)
        {
            _args.Clear();
            List<string> parameters = new List<string>();

            int qouteStart = 0;
            bool isInqoutes = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    if (!isInqoutes)
                    {
                        qouteStart = i;
                        isInqoutes = true;
                    }
                    else
                    {
                        parameters.Add(input.Substring(qouteStart + 1, i - qouteStart - 1));
                        input = input.Remove(qouteStart, i - qouteStart + 1);
                    }
                }
            }
            _args = !string.IsNullOrWhiteSpace(input) ? input.Split(' ').ToList() : new List<string>();
            _args.AddRange(parameters);
        }

        private void WriteHelp()
        {
            Console.WriteLine($@"
Just drag a files over here or write file paths to read and convert files.

Use the {textCommand} command to convert text instead of a text from a file.
Use the {clipBoardCommand} command to copy the output to the clipboard instead of to a file.
Use the {bbcCommand} command to convert the text without BB code applied.

Use the {reloadCommand} command to reload the lua files.
Use the {clearCommand} command to clear the window.

Use the {quitCommand} command to quit this application.
Use the {helpCommand} command to open this help screen.");
        }

        private bool ReadArguments()
        {
            _ranCommand = false;

            _args.RemoveAll(a => string.IsNullOrWhiteSpace(a));

            if (!ReadCommands())
            {
                return false;
            }

            if (!_ranCommand && _args.Count == 0)
            {
                Console.WriteLine($"No text found to convert. Are you sure you typed something in?");

                return false;
            }

            for (int i = 0; i < _args.Count; i++)
            {
                if (!_isTextMode)
                {
                    if (!ConvertFromFile(_args[i]))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!ConvertText(_args[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ReadCommands()
        {
            bool hasError = false;
            for (int i = _args.Count - 1; i >= 0; i--)
            {
                string arg = _args[i].ToLower();

                _ranCommand = true;

                if (arg.StartsWith("-"))
                {
                    switch (arg)
                    {
                        case textCommand:
                            if (!_isTextMode)
                            {
                                _isTextMode = true;
                                _args.RemoveAt(i);
                            }
                            else
                            {
                                hasError = true;
                            }

                            break;
                        case clipBoardCommand:
                            if (!_isClipBoardMode)
                            {
                                Clipboard.Clear();
                                _isClipBoardMode = true;
                                _args.RemoveAt(i);
                            }
                            else
                            {
                                hasError = true;
                            }

                            break;
                        case helpCommand:
                            WriteHelp();
                            _args.Clear();

                            return true;
                        case quitCommand:
                            if (!_quit)
                            {
                                _quit = true;
                                _args.Clear();
                                return true;
                            }
                            else
                            {
                                hasError = true;
                            }

                            break;
                        case bbcCommand:
                            if (_isBbcMode)
                            {
                                _isBbcMode = false;
                                _args.RemoveAt(i);
                            }
                            else
                            {
                                hasError = true;
                            }

                            break;
                        case reloadCommand:
                            LoadLua();
                            _args.Clear();

                            break;
                        case clearCommand:
                            Console.Clear();
                            WriteStartText();
                            _args.Clear();

                            break;
                        default:
                            Console.WriteLine($"Unknown command {arg}. Use the {helpCommand} command to get a list of commands.");
                            _args.Clear();
                            break;
                    }
                }

                if (hasError)
                {
                    Console.WriteLine($"Error setting command {arg}. Is this command already set?");
                    break;
                }
            }

            return !hasError;
        }

        private bool ConvertFromFile(string path)
        {
            if (!_isTextMode)
            {
                string text = "";
                bool result = false;

                try
                {
                    using (StreamReader sr = File.OpenText(path))
                    {
                        text = sr.ReadToEnd();
                        result = _converter.ConvertText(ref text, true);
                    }
                    _currentFileName = Path.GetFileNameWithoutExtension(path);
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine(e.Message);
#else
                    Console.WriteLine( path + " is not a valid path!");
#endif

                    return false;
                }

                if (result)
                {
                    result = Output(text);
                }
                return result;
            }
            else
            {
                Console.WriteLine("Can't convert from file after reading text with -t!");
                return false;
            }
        }

        private bool ConvertText(string text)
        {
            if (_isTextMode)
            {
                bool result = _converter.ConvertText(ref text, _isBbcMode);

                if (result)
                {
                    result = Output(text);
                }
                return result;

            }
            else
            {
                Console.WriteLine("Already loaded text from a file!");
                return false;
            }
        }

        private bool Output(string text)
        {
            if (!_isClipBoardMode)
            {
                return SaveToFile(text);
            }
            else
            {
                return SaveToClipoard(text);
            }
        }

        private bool SaveToFile(string output)
        {
            DateTime date = DateTime.Now;
            StringBuilder sb = new StringBuilder();

            sb.Append(_outputPath);
            sb.Append(date.ToString("dd-MM-yyyy"));

            Directory.CreateDirectory(sb.ToString());
            sb.Append("\\");
            sb.Append(string.IsNullOrWhiteSpace(_currentFileName) ? date.ToString("dd-MM-yyyy HH-mm-ss") : _currentFileName);
            sb.Append(".txt");

            try
            {
                using (FileStream stream = new FileStream(sb.ToString(), FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.Write(output);
                    }
                    Console.WriteLine("Text saved to file in: " + sb.ToString());
                }
            }
            catch
            {
                Console.WriteLine("Unable to save file to: " + sb.ToString());
                return false;
            }

            return true;
        }

        private bool SaveToClipoard(string output)
        {
            if (!string.IsNullOrWhiteSpace(output))
            {
                string clipBoardText = Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(clipBoardText))
                {
                    Clipboard.SetText($"{clipBoardText}\n{output}");
                }
                else
                {
                    Clipboard.SetText(output);
                }
                Console.WriteLine("Text copied to clipboard!");
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
