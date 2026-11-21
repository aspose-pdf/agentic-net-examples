using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string texPath = "input.tex";
        const string pdfPath = "output.pdf";
        const string intermediatePdf = "intermediate.pdf";

        // Verify the LaTeX source file exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"LaTeX file not found: {texPath}");
            return;
        }

        // Ensure that the LaTeX engine (pdflatex) is available on the system PATH
        if (!IsToolAvailable("pdflatex"))
        {
            Console.Error.WriteLine("pdflatex executable not found in PATH. Install a LaTeX distribution (e.g., TeX Live or MiKTeX) and ensure pdflatex is reachable.");
            return;
        }

        // Run pdflatex to compile the .tex file to PDF (produces intermediate.pdf in the same folder)
        var startInfo = new ProcessStartInfo
        {
            FileName = "pdflatex",
            Arguments = $"-interaction=nonstopmode -halt-on-error -output-directory=.",
            WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(texPath)) ?? ".",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // pdflatex expects the file name only (no path) when the working directory is set
        startInfo.Arguments += " " + Path.GetFileName(texPath);

        using (var process = Process.Start(startInfo))
        {
            if (process == null)
            {
                Console.Error.WriteLine("Failed to start pdflatex process.");
                return;
            }

            string stdOut = process.StandardOutput.ReadToEnd();
            string stdErr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.Error.WriteLine("pdflatex failed to compile the LaTeX document.");
                Console.Error.WriteLine(stdOut);
                Console.Error.WriteLine(stdErr);
                return;
            }
        }

        // The generated PDF will have the same base name as the .tex file
        string generatedPdf = Path.ChangeExtension(texPath, ".pdf");
        if (!File.Exists(generatedPdf))
        {
            Console.Error.WriteLine($"Expected PDF not found after compilation: {generatedPdf}");
            return;
        }

        // Load the generated PDF with Aspose.Pdf, optionally apply further processing, then save to the desired output path
        Document pdfDoc = new Document(generatedPdf);
        pdfDoc.Save(pdfPath);

        // Clean up the intermediate PDF if you don't need it
        try
        {
            File.Delete(generatedPdf);
        }
        catch { /* ignore cleanup errors */ }

        Console.WriteLine($"LaTeX successfully converted to PDF: {pdfPath}");
    }

    /// <summary>
    /// Checks whether a given executable is available on the system PATH.
    /// </summary>
    private static bool IsToolAvailable(string toolName)
    {
        var whichCmd = Environment.OSVersion.Platform == PlatformID.Win32NT ? "where" : "which";
        var startInfo = new ProcessStartInfo
        {
            FileName = whichCmd,
            Arguments = toolName,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using (var proc = Process.Start(startInfo))
        {
            if (proc == null) return false;
            proc.WaitForExit();
            return proc.ExitCode == 0;
        }
    }
}
