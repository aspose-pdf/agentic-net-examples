using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_print.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Attach a JavaScript action that opens the print dialog when the PDF is opened
            // The script "this.print(true);" triggers the print dialog automatically
            doc.OpenAction = new JavascriptAction("this.print(true);");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑print action: '{outputPath}'");
    }
}