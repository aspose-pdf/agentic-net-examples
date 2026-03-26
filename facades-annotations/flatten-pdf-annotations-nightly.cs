using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        string taskName = "FlattenPDFAnnotationsNightly";
        string sharePath = @"\\server\share\pdfs";

        if (args != null && args.Length > 0 && args[0] == "install")
        {
            InstallScheduledTask(taskName, sharePath);
            Console.WriteLine("Scheduled task installed.");
            return;
        }

        FlattenAnnotationsInFolder(sharePath);
    }

    private static void InstallScheduledTask(string taskName, string sharePath)
    {
        string exePath = Process.GetCurrentProcess().MainModule.FileName;
        string arguments = $"\"{exePath}\"";
        // Schedule daily at 02:00 AM
        string scheduleCommand = $"/Create /SC DAILY /TN \"{taskName}\" /TR \"{arguments}\" /ST 02:00 /F";
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "schtasks",
            Arguments = scheduleCommand,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        using (Process process = Process.Start(startInfo))
        {
            process.WaitForExit();
        }
    }

    private static void FlattenAnnotationsInFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfFilePath in pdfFiles)
        {
            string directory = Path.GetDirectoryName(pdfFilePath);
            string fileName = Path.GetFileName(pdfFilePath);

            string originalDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(directory);

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(fileName);
                editor.FlatteningAnnotations();
                editor.Save(fileName);
            }

            Directory.SetCurrentDirectory(originalDirectory);
            Console.WriteLine("Flattened: " + fileName);
        }
    }
}