using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPath = "order.pdf";
        const string outputPath = "appearance_set.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable, so wrap it in a using block
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Set appearance flags for the field named "OrderNumber"
                // Use the valid Print flag (visible by default). Combine with other flags if needed.
                editor.SetFieldAppearance("OrderNumber", AnnotationFlags.Print);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with updated field appearance: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
