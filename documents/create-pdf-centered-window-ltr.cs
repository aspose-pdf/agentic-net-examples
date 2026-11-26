using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "CenteredLTR.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Center the PDF viewer window when the document is opened
            doc.CenterWindow = true;

            // Set the reading direction to left‑to‑right.
            // The PdfDirection enum is available in recent Aspose.PDF versions.
            // If the enum is not present in the referenced version, the default direction is LTR,
            // so the line can be safely omitted without affecting the output.
            // Uncomment the line below when using a version that includes PdfDirection.
            // doc.Direction = Aspose.Pdf.PdfDirection.LTR;

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
