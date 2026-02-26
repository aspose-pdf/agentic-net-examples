using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input_form.pdf";   // PDF with AcroForm fields
        const string outputPdfPath  = "output_form.pdf";  // PDF after importing FDF data
        const string xmlOutputPath  = "form_fields.xml"; // Destination for exported XML
        const string fdfInputPath   = "data.fdf";        // FDF file containing field values

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfInputPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {fdfInputPath}");
            return;
        }

        // Work with the AcroForm using the Form facade
        using (Form form = new Form(inputPdfPath)) // loads the PDF
        {
            // ---------- Export form fields to XML ----------
            using (FileStream xmlStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream); // writes XML representation of the form fields
            }

            // ---------- Import field values from FDF ----------
            using (FileStream fdfStream = new FileStream(fdfInputPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream); // populates the PDF form with data from the FDF
            }

            // ---------- Save the modified PDF ----------
            form.Save(outputPdfPath); // writes the updated PDF to the specified path
        }

        Console.WriteLine("Export to XML, import from FDF, and PDF save completed successfully.");
    }
}