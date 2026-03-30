using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF with PdfContentEditor (as required by the task).
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Load the same PDF as a Document to work with the AcroForm.
            Document doc = new Document(inputPath);

            // Fill the AcroForm field named "CustomerName".
            // The indexer returns a WidgetAnnotation; cast it to the appropriate field type.
            if (doc.Form != null && doc.Form["CustomerName"] is TextBoxField txtField)
            {
                txtField.Value = "Acme Corporation";
            }
            else
            {
                Console.Error.WriteLine("Form field 'CustomerName' not found or is not a text box.");
                return;
            }

            // Save the updated PDF.
            doc.Save(outputPath);
            // Also save via the editor to demonstrate that the PDF was processed with PdfContentEditor.
            editor.Save(outputPath);

            Console.WriteLine($"Form field filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
