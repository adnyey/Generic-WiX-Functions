using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.IO;

namespace GenericWixCustomActions
{
    public class CustomActions
    {
        /*
         * FUNCTION SUMMARY: Executes a cmd in background and captures its output in session logs
         * CMDFILENAME = Absolute path to the executable
         * CMDEXEPARAMS = Parameters for executable
         * CMDDIRECTORY = Directory in which to execute the command
         */
        [CustomAction]
        public static ActionResult QuietCmd(Session session)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = session.CustomActionData["CMDFILENAME"];
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.Verb = "runas";
                startInfo.CreateNoWindow = true;
                startInfo.WorkingDirectory = session.CustomActionData["CMDDIRECTORY"];
                startInfo.Arguments = session.CustomActionData["CMDEXEPARAMS"];

                // Log the received parameters
                session.Log(
                    "\nExecutable: " + startInfo.FileName
                    + "\nArguments: " + startInfo.Arguments
                    + "\nWorkingDirectory: " + startInfo.WorkingDirectory);

                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (output.Length > 0)
                    session.Log("QuietCmd() -> \n" + output);

                if (process.ExitCode > 0)
                {
                    // Do what you want here
                    return ActionResult.Failure;
                }

                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("QuietCmd() -> Exception : {0}\r\n Trace: {1}", ex.Message, ex.StackTrace);
            }
            return ActionResult.Failure;
        }
    }
}
