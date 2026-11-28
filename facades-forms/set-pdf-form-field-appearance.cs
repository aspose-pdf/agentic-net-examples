using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "contract_form_v4.pdf";
        const string outputPath = "approval_appearance.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF into FormEditor
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                // Set the appearance of the "Approval" field using AnnotationFlags
                // Example: make the field printable
                editor.SetFieldAppearance("Approval", AnnotationFlags.Print);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Saved PDF with updated field appearance to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}