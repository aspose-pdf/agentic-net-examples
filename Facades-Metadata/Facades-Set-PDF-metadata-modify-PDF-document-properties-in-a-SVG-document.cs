using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class SetPdfMetadataFromSvg
{
    static void Main(string[] args)
    {
        // Input SVG file and output PDF file paths.
        // Adjust these paths as needed.
        const string svgPath = "input.svg";
        const string pdfPath = "output.pdf";

        // Verify that the SVG source file exists.
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"Error: SVG file not found at '{svgPath}'.");
            return;
        }

        try
        {
            // Load the SVG and convert it to a PDF document.
            // SvgLoadOptions uses the default conversion engine.
            var loadOptions = new SvgLoadOptions();
            using (Document pdfDocument = new Document(svgPath, loadOptions))
            {
                // Use the Form facade to work with the PDF document.
                // The Form constructor that accepts a Document binds it automatically.
                using (Form form = new Form(pdfDocument))
                {
                    // Modify PDF metadata via the underlying Document's Info property.
                    // Any of the standard metadata fields can be set here.
                    form.Document.Info.Title = "Sample SVG‑to‑PDF Document";
                    form.Document.Info.Author = "Aspose.Pdf Facades Example";
                    form.Document.Info.Subject = "Demonstration of setting PDF metadata";
                    form.Document.Info.Keywords = "SVG, PDF, metadata, Aspose.Pdf.Facades";
                    form.Document.Info.Creator = "SetPdfMetadataFromSvg";

                    // Save the modified PDF to the specified output file.
                    form.Save(pdfPath);
                }
            }

            Console.WriteLine($"PDF successfully created with metadata at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}