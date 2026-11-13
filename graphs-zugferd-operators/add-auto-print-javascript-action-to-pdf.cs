using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "print_on_load.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add a JavaScript action that opens the print dialog when the document is opened
                // The script uses the PDF JavaScript API: this.print(...)
                doc.OpenAction = new JavascriptAction("this.print({bUI:true,bSilent:false,bShrinkToFit:true});");

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with auto‑print JavaScript: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
