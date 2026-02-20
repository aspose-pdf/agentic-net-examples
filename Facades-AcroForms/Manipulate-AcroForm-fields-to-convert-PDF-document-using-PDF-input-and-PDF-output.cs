using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Step 1: Add a new AcroForm field using FormEditor
            // -----------------------------------------------------------------
            using (FormEditor formEditor = new FormEditor())
            {
                // Bind the source PDF to the editor
                formEditor.BindPdf(inputPdfPath);

                // Add a text field named "NewField" on page 1
                // Parameters: field type, field name, page number, llx, lly, urx, ury
                // Correct enum value for a text field is FieldType.Text
                formEditor.AddField(FieldType.Text, "NewField", 1, 100, 100, 200, 120);

                // Save the intermediate PDF (with the new field) to a temporary stream
                using (MemoryStream tempStream = new MemoryStream())
                {
                    formEditor.Save(tempStream);
                    tempStream.Position = 0; // Reset stream for next operation

                    // -----------------------------------------------------------------
                    // Step 2: Fill fields and flatten using Form facade
                    // -----------------------------------------------------------------
                    using (Form form = new Form())
                    {
                        // Bind the PDF (now containing the new field) from the stream
                        form.BindPdf(tempStream);

                        // Fill an existing field (if present) and the newly added field
                        try { form.FillField("Name", "John Doe"); } catch { /* ignore if field not present */ }
                        form.FillField("NewField", "Sample Text");

                        // Flatten all fields to make them part of the page content
                        form.FlattenAllFields();

                        // Save the final PDF to the desired output path
                        form.Save(outputPdfPath);
                    }
                }
            }

            Console.WriteLine($"PDF processing completed. Output saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during PDF processing: {ex.Message}");
        }
    }
}
