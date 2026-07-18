using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "print_on_open.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // JavaScript that opens the print dialog when the document is opened
            // "this.print(true);" shows the dialog and forces printing
            JavascriptAction jsAction = new JavascriptAction("this.print(true);");

            // Assign the JavaScript as the document's open action
            doc.OpenAction = jsAction;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with auto‑print on open.");
    }
}