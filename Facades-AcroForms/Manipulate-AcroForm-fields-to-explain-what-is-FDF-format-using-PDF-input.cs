using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and optional output folder (second argument)
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputFolder = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();

        // Validate input PDF
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Paths for the exported FDF file and a copy of the PDF after import
        string fdfPath = Path.Combine(outputFolder, "exported.fdf");
        string importedPdfPath = Path.Combine(outputFolder, "imported.pdf");

        // -----------------------------------------------------------------
        // 1. Export AcroForm field data to FDF using the Form facade
        // -----------------------------------------------------------------
        using (Form form = new Form())
        {
            // Bind the PDF document to the Form facade
            form.BindPdf(pdfPath);

            // Export fields to a memory stream
            using (MemoryStream fdfStream = new MemoryStream())
            {
                form.ExportFdf(fdfStream);
                // Write the FDF content to a file for inspection
                File.WriteAllBytes(fdfPath, fdfStream.ToArray());

                // Display a short explanation of the FDF format
                Console.WriteLine("FDF (Forms Data Format) is a lightweight file format used to exchange");
                Console.WriteLine("form field values between PDF documents and external applications.");
                Console.WriteLine("It contains only the names of the fields and their corresponding values.");
                Console.WriteLine($"Exported FDF saved to: {fdfPath}");
                Console.WriteLine();
                Console.WriteLine("=== FDF Content Preview ===");
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(fdfStream.ToArray()));
                Console.WriteLine("=== End of FDF Preview ===");
                Console.WriteLine();

                // -----------------------------------------------------------------
                // 2. Demonstrate importing the same FDF back into a copy of the PDF
                // -----------------------------------------------------------------
                // Create a copy of the original PDF to import the FDF into
                File.Copy(pdfPath, importedPdfPath, true);

                using (Form importForm = new Form())
                {
                    importForm.BindPdf(importedPdfPath);
                    // Reset the stream position before import
                    fdfStream.Position = 0;
                    importForm.ImportFdf(fdfStream);
                    // Save the PDF with imported values
                    importForm.Save(importedPdfPath);
                }

                Console.WriteLine($"FDF data imported back into a copy of the PDF: {importedPdfPath}");
            }
        }

        Console.WriteLine("Operation completed successfully.");
    }
}