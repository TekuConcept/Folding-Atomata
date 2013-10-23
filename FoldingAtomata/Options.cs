using System;
using System.Collections.Generic;
using System.IO;

namespace FoldingAtomata
{
    public class Options
    {
        static Options singleton_ = null;

        #region - Public -
        public static Options GetInstance()
        {
            if (singleton_ == null)
                singleton_ = new Options();
            return singleton_;
        }
        public static bool HandleFlags(int argc, string[] argv)
        {
            List<string> options = new List<string>();
            for (int j = 0; j < argc; j++)
                options.Add(argv[j]);

            int index = 1; //skips the name-of-program argument
            while (index < (int)argc)
            {
                if (options[index] == "--help" || options[index] == "-h")
                {
                    Console.Write(@"
                            Usage:
                                FAHViewer [OPTION...]

                            Commands:
                                --help,     -h      Show usage and flag options.
                                --connect,  -c      Address and port to use to connect to FAHClient.
                                --license           Prints license information.
                                --mode,     -m      Rendering mode. 3 is stick, everything is ball-n-stick.
                                --password, -p      Password for accessing the remote FAHClient.
                                --slices,   -sl     Slices to use for the atom mesh. Default is 8.
                                --stacks,   -st     Stacks to use for the atom mesh. Default is 16.
                                --verbose,  -v      Verbose printing to stdout.
                                --version           Print version information.

                            Examples:
                                FoldingAtomata
                                FoldingAtomata --connect=203.0.113.0:36330 --password=example
                            ");
                    return false;
                }

                if (options[index] == "--version")
                {
                    Console.Write("0.6.8.0\n");
                    return false;
                }

                if (options[index] == "--license")
                {
                    Console.Write("GPLv3+\n");
                    return false;
                }

                index += GetInstance().Handle(ref options, index);
            }

            return true;
        }

        public Options()
        {
            HighVerbosity = true;
            CycleSnapshots = true;
            SkyboxDisabled = false;
            ConnectionPath = "127.0.0.1:36330";
            PathToImageA = Path.Combine(Environment.CurrentDirectory, @"\images\gradient.png");
            PathToImageB = Path.Combine(Environment.CurrentDirectory, @"\images\gradient.png");
            PathToImageC = Path.Combine(Environment.CurrentDirectory, @"\images\gradient.png");
            AtomStacks = 8;
            AtomSlices = 16;
            AnimationDelay = 40;

            Host = "127.0.0.1";
            Port = 36330;
            RenderMode = RenderMode.BALL_N_STICK;
        }

        public bool HighVerbosity { get; private set; }
        public bool BounceSnapshots { get; private set; }
        public bool CycleSnapshots { get; private set; }
        public bool UsesPassword { get; private set; }
        public bool SkyboxDisabled { get; set; }
        public int Port { get; private set; }
        public int AnimationDelay { get; private set; }
        public int AtomStacks { get; private set; }
        public int AtomSlices { get; private set; }
        public String Password { get; private set; }
        public String Host { get; private set; }
        public String ConnectionPath { get; private set; }
        public String PathToImageA { get; set; }
        public String PathToImageB { get; set; }
        public String PathToImageC { get; set; }
        public RenderMode RenderMode { get; private set; }
        #endregion
        
        #region - Private A -
        int Handle(ref List<string> options, int index)
        {
            string flag = options[index];

            //check for 1-piece flags
            if (Verbose1(ref flag) || Connect1(ref flag) || BounceSnapshots1(ref flag) ||
                CycleSnapshots1(ref flag) || Password1(ref flag) || RenderMode1(ref flag) ||
                AtomStacks1(ref flag) || AtomSlices1(ref flag)
            )
                return 1;

            //check to see if we can grab next flag
            if (index + 1 >= options.Count)
            {
                Console.Write("Unrecognized flag or unavailable argument for {0}. Ignoring.", flag);
                return 2;
            }

            //check for two-piece flags
            string arg = options[index + 1];
            if (Connect2(ref flag, ref arg) || BounceSnapshots2(ref flag, ref arg) ||
                CycleSnapshots2(ref flag, ref arg) || Password2(ref flag, ref arg) ||
                RenderMode2(ref flag, ref arg) || AtomStacks2(ref flag, ref arg) || AtomSlices2(ref flag, ref arg)
            )
                return 2;

            Console.Write("Unrecognized flag {0}. Ignoring.\n", options[index]);
            return 1;
        }
        bool Verbose1(ref string flag)
        {
            //test for verbosity flags
            if (flag == "--verbose" || flag == "-v")
            {
                HighVerbosity = true;
                return true;
            }

            return false;
        }
        bool Connect1(ref string flag)
        {
            if (flag.StartsWith("--connect="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                var parameters = parts[1].Split(':');
                if (!Confirm(parameters.Length == 2, ref flag))
                    return false;

                Host = parameters[0];
                Port = int.Parse(parameters[1]);
                return true;
            }

            return false;
        }
        bool Connect2(ref string flag, ref string arg)
        {
            if (flag == "--connect" || flag == "-c")
            {
                var tokens = arg.Split(':');
                if (!Confirm(tokens.Length == 2, ref flag))
                    return false;

                Host = tokens[0];
                Port = int.Parse(tokens[1]);
                return true;
            }

            return false;
        }
        bool BounceSnapshots1(ref string flag)
        {
            if (flag.StartsWith("--bounce-snapshots="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                if (!Confirm(parts[1] == "true" || parts[1] == "false", ref flag))
                    return false;
                
                BounceSnapshots = bool.Parse(parts[1]); // *
                return true;
            }

            return false;
        }
        bool BounceSnapshots2(ref string flag, ref string arg)
        {
            if (flag == "--bounce-snapshots" || flag == "-b")
            {
                string next = arg;
                if (!Confirm(next == "true" || next == "false", ref flag))
                {
                    BounceSnapshots = true; //just the flag was given
                    return true;
                }

                BounceSnapshots = bool.Parse(next);
                return true;
            }

            return false;
        }
        bool CycleSnapshots1(ref string flag)
        {
            if (flag.StartsWith("--cycle-snapshots="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                if (!Confirm(parts[1] == "true" || parts[1] == "false", ref flag))
                    return false;

                CycleSnapshots = bool.Parse(parts[1]);
                return true;
            }

            return false;
        }
        bool CycleSnapshots2(ref string flag, ref string arg)
        {
            if (flag == "--cycle-snapshots" || flag == "-b")
            {
                string next = arg;
                if (!Confirm(next == "true" || next == "false", ref flag))
                {
                    CycleSnapshots = true; //just the flag was given
                    return false;
                }

                CycleSnapshots = bool.Parse(next);
                return true;
            }

            return false;
        }
        bool Password1(ref string flag)
        {
            if (flag.StartsWith("--password="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                Password = parts[1].Trim('\"'); // *
                UsesPassword = true;
                return true;
            }

            return false;
        }
        bool Password2(ref string flag, ref string arg)
        {
            if (flag == "--password" || flag == "-p")
            {
                Password = arg.Trim('\"'); // *
                UsesPassword = true;
                return true;
            }

            return false;
        }
        bool RenderMode1(ref string flag)
        {
            if (flag.StartsWith("--mode="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                if (parts[1] == "4") RenderMode = RenderMode.BALL_N_STICK;
                else if (parts[1] == "3") RenderMode = RenderMode.STICK;
                else RenderMode = RenderMode.BALL_N_STICK;

                return true;
            }

            return false;
        }
        bool RenderMode2(ref string flag, ref string arg)
        {
            if (flag == "--mode" || flag == "-m")
            {
                if (arg == "4") RenderMode = RenderMode.BALL_N_STICK;
                else if (arg == "3") RenderMode = RenderMode.STICK;
                else RenderMode = RenderMode.BALL_N_STICK;

                return true;
            }

            return false;
        }
        bool AtomStacks1(ref string flag)
        {
            if (flag.StartsWith("--stacks="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                AtomStacks = int.Parse(parts[1]);
                return true;
            }

            return false;
        }
        bool AtomStacks2(ref string flag, ref string arg)
        {
            if (flag == "--stacks" || flag == "-st")
            {
                AtomStacks = int.Parse(arg);
                return true;
            }

            return false;
        }
        bool AtomSlices1(ref string flag)
        {
            if (flag.StartsWith("--slices="))
            {
                var parts = flag.Split('=');
                if (!Confirm(parts.Length == 2, ref flag))
                    return false;

                AtomSlices = int.Parse(parts[1]);
                return true;
            }

            return false;
        }
        bool AtomSlices2(ref string flag, ref string arg)
        {
            if (flag == "--slices" || flag == "-sl")
            {
                AtomSlices = int.Parse(arg);
                return true;
            }

            return false;
        }
        bool Confirm(bool condition, ref string flag)
        {
            if (!condition)
            {
                Console.Write("Invalid parameters for {0}\n", flag);
                return false;
            }

            return true;
        }
        #endregion
    }

    public enum RenderMode
    {
        BALL_N_STICK,
        STICK,
    }
}