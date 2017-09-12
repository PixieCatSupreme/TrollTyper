using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrollTyper.TrollQuirks;

namespace TrollTyper
{
    class TrollTyper
    {
        private const string textCommand = "-t";
        private const string clipBoardCommand = "-c";
        private const string helpCommand = "-h";

        private readonly string _currentPath;

        private Converter _converter;
        private bool _isInFileMode;
        private bool _isInputMode;
        private bool _isClipBoardMode;
        private int _index;

        private string[] _args;
        private string _currentFileName;

        public TrollTyper(string[] args)
        {
            _args = args;
            _currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Output\";
            _currentFileName = "";

            _isInFileMode = false;
            _isInputMode = false;
            _isClipBoardMode = false;

            _converter = new Converter
                (
                    new MitinaQuirk()
                );
        }

        public bool Run()
        {
            Console.WriteLine("Welcome to Trolltyper! The application to change your text into Homestuck style logs.\n");

            if (_args.Length > 0 && !_args.Contains(helpCommand))
            {
                return ReadArguments();
            }
            else
            {
                Console.WriteLine(string.Format(@"Just drag a files over here or write file paths to read and convert files.
Use the {0} command IN FRONT OF text to convert that instead of a file.
Use the {1} command IN FRONT OF all other commands to copy the output to the clipboard instead of to a file.
Use the {2} command to open this help screen.",
textCommand, clipBoardCommand, helpCommand));
                return false;
            }
        }

        private bool ReadArguments()
        {
            for (_index = 0; _index < _args.Length; _index++)
            {
                switch (_args[_index].ToLower())
                {
                    case textCommand:
                        if (!ConvertText())
                        {
                            return false;
                        }
                        break;
                    case clipBoardCommand:
                        if (!SetClipboardMode())
                        {
                            return false;
                        }
                        break;

                    default:
                        _isInFileMode = true;
                        if (!ConvertFromFile())
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        private bool ConvertFromFile()
        {
            if (!_isInputMode)
            {
                string path = _args[_index];
                string text = "";
                bool result = false;

                try
                {
                    using (StreamReader sr = File.OpenText(path))
                    {
                        text = sr.ReadToEnd();
                        result = _converter.ConvertText(ref text);
                    }
                    _currentFileName = Path.GetFileNameWithoutExtension(path);
                }
                catch
                {
                    Console.WriteLine( path + " is not a valid path!");
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
                Console.WriteLine("Already loaded text after -t command!");
                return false;
            }
        }

        private bool ConvertText()
        {
            _index++;
            if (!_isInFileMode && !_isInputMode)
            {
                if (_index < _args.Length)
                {
                    string text = _args[_index];
                    bool result = _converter.ConvertText(ref text);

                    if (result)
                    {
                        result = Output(text);
                    }
                    return result;
                }
                else
                {
                    Console.WriteLine("No text found after -t command!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Already loaded text from a file!");
                return false;
            }
        }

        private bool SetClipboardMode()
        {
            _index++;
            if (_index < _args.Length)
            {
                if (!_isClipBoardMode)
                {
                    _isClipBoardMode = true;
                    return true;
                }
                else
                {
                    Console.WriteLine("Unexpected command " + _args[_index] + "!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("No text found after -c command!\nPlace this command in front of all toehr commands to make it work!");
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

            sb.Append(_currentPath);
            sb.Append(date.ToString("dd-MM-yyyy"));

            Directory.CreateDirectory(sb.ToString());
            sb.Append("\\");
            sb.Append(string.IsNullOrWhiteSpace(_currentFileName) ? date.ToString("dd-MM-yyyy HH-mm-ss") : _currentFileName);
            sb.Append(".txt");

            try
            {
                using (StreamWriter sw = File.CreateText(sb.ToString()))
                {
                    sw.Write(output);
                }
                Console.WriteLine("Text saved to file in: " + sb.ToString());
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
                Clipboard.SetText(output);
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
