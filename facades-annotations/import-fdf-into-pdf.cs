using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF with form fields
        const string fdfPath = "data.fdf";    // FDF file containing field values
        const string outPath = "filled_output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Use the Form facade to import FDF data into the PDF
                using (Form form = new Form())
                {
                    // Bind the PDF document to the Form object
                    form.BindPdf(pdfDoc);

                    // Import field values from the FDF file (ImportFdf expects a Stream)
                    using (FileStream fdfStream = File.OpenRead(fdfPath))
                    {
                        form.ImportFdf(fdfStream);
                    }
                }

                // Save the updated PDF
                pdfDoc.Save(outPath);
            }

            Console.WriteLine($"Form fields imported from '{fdfPath}' and saved to '{outPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
