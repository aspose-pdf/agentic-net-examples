using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // This entry point is intentionally minimal.
        // The actual batch text‑extraction logic is implemented in the PowerShell script
        // "Extract-PdfText.ps1" which uses Aspose.Pdf.Facades.PdfExtractor.
        // Keeping this C# project compile‑able allows the solution to build and
        // optionally host the PowerShell script via System.Management.Automation if desired.
        Console.WriteLine("Aspose.Pdf batch extractor ready. Run the PowerShell script to process PDFs.");
    }
}
