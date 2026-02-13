using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string outputFdfPath = "output.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Access the AcroForm object
            Form pdfForm = pdfDocument.Form;

            // Export form field data to an FDF file
            using (StreamWriter writer = new StreamWriter(outputFdfPath, false, System.Text.Encoding.ASCII))
            {
                // Write minimal FDF header
                writer.WriteLine("%FDF-1.2");
                writer.WriteLine("%âãÏÓ");
                writer.WriteLine("1 0 obj");
                writer.WriteLine("<< /FDF << /Fields [");

                // Iterate over all fields in the form
                foreach (Field field in pdfForm)
                {
                    string fieldName = field.FullName ?? "";
                    string fieldValue = field.Value?.ToString() ?? "";

                    // Escape parentheses in name/value
                    fieldName = fieldName.Replace("(", "\\(").Replace(")", "\\)");
                    fieldValue = fieldValue.Replace("(", "\\(").Replace(")", "\\)");

                    writer.WriteLine($"<< /T ({fieldName}) /V ({fieldValue}) >>");
                }

                // Close the Fields array and the dictionaries
                writer.WriteLine("] >> >>");
                writer.WriteLine("endobj");
                writer.WriteLine("trailer");
                writer.WriteLine("<< /Root 1 0 R >>");
                writer.WriteLine("%%EOF");
            }

            // Save the (unchanged) PDF document to the output path
            pdfDocument.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}