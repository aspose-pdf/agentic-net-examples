using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document (or load an existing one)
        using (Document doc = new Document())
        {
            // Add a blank page so the document is not empty
            doc.Pages.Add();

            // JavaScript that saves the PDF every 30 seconds (30000 ms)
            string jsCode = "app.setInterval(function() { this.saveAs({cPath: 'autosave.pdf'}); }, 30000);";

            // Assign the script as a document‑level open action
            doc.OpenAction = new JavascriptAction(jsCode);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with document‑level JavaScript saved as output.pdf");
    }
}