using System;
using System.IO;
using System.Text;
using Aspose.Pdf;                     // Document class
using Aspose.Pdf.Facades;            // Form class

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input_form.pdf";      // source PDF with form fields
        const string outputPdfPath = "output_form.pdf";     // PDF after import
        const string xfdfPath      = "form_fields.xfdf";    // temporary XFDF file

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the Form facade on the loaded document
            Form form = new Form(pdfDoc);

            // ---------- Export form field values to XFDF using UTF‑8 ----------
            // Create a file stream for writing and wrap it with a StreamWriter that uses UTF‑8 encoding.
            // The Form.ExportXfdf method expects a raw Stream, so we pass the underlying BaseStream.
            using (FileStream exportStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(exportStream, Encoding.UTF8))
            {
                // Export the current field values to the XFDF stream.
                form.ExportXfdf(writer.BaseStream);
                writer.Flush(); // Ensure all data is written before closing the stream
            }

            // At this point you could modify the XFDF file externally if needed.
            // For demonstration, we will import the same XFDF back into the PDF.

            // ---------- Import form field values from XFDF using UTF‑8 ----------
            using (FileStream importStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(importStream, Encoding.UTF8))
            {
                // Import the XFDF data back into the PDF form.
                form.ImportXfdf(reader.BaseStream);
            }

            // Save the modified PDF document to the desired output path.
            // The Document.Save method is used (lifecycle rule) – no custom saving logic.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields exported to '{xfdfPath}' and re‑imported. Output saved to '{outputPdfPath}'.");
    }
}