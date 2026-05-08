using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "print_on_open.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a single blank page (optional, ensures the PDF has content)
            doc.Pages.Add();

            // JavaScript code that opens the print dialog when the document is opened
            string jsCode = "this.print(true);";

            // Create the JavaScript action
            JavascriptAction printAction = new JavascriptAction(jsCode);

            // Assign the action to the document's OpenAction so it runs on load
            doc.OpenAction = printAction;

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with auto‑print on open.");
    }
}