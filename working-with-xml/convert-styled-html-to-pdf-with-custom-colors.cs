using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a temporary working directory inside the sandbox.
        string workDir = Path.Combine(Path.GetTempPath(), "AsposePdfDemo");
        Directory.CreateDirectory(workDir);

        // ---------------------------------------------------------------------
        // 1. Create a sample HTML file that contains inline style information.
        // ---------------------------------------------------------------------
        string htmlFile = Path.Combine(workDir, "StyledDocument.html");
        string htmlContent = @"<?xml version='1.0' encoding='utf-8'?>
<html>
  <body>
    <p style='color:#FF0000; font-size:16pt;'>Hello, styled PDF!</p>
    <p style='color:#0000FF;'>This paragraph is blue.</p>
  </body>
</html>";
        File.WriteAllText(htmlFile, htmlContent);

        // ---------------------------------------------------------------------
        // 2. Convert the HTML (with its style definitions) to PDF.
        // ---------------------------------------------------------------------
        string pdfFile = Path.Combine(workDir, "StyledDocument.pdf");
        HtmlLoadOptions htmlLoadOptions = new HtmlLoadOptions(); // Handles inline CSS.
        using (Document pdfDocument = new Document(htmlFile, htmlLoadOptions))
        {
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF with custom color scheme saved to '{pdfFile}'.");
    }
}
