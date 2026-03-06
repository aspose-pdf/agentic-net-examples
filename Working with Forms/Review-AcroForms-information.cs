using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form acroForm = doc.Form;

            // Print basic form information
            Console.WriteLine($"Form Type: {acroForm.Type}");               // Standard, Static, or Dynamic
            Console.WriteLine($"Has XFA: {acroForm.HasXfa}");
            Console.WriteLine($"Needs Rendering (XFA): {acroForm.NeedsRendering}");
            Console.WriteLine($"Number of fields: {acroForm.Count}");

            // Iterate over all fields in the form
            foreach (Field field in acroForm.Fields)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Full Name   : {field.FullName}");
                Console.WriteLine($"Partial Name: {field.PartialName}");
                Console.WriteLine($"Value       : {field.Value}");
                Console.WriteLine($"ReadOnly    : {field.ReadOnly}");
                Console.WriteLine($"Required    : {field.Required}");
                Console.WriteLine($"Exportable  : {field.Exportable}");
                Console.WriteLine($"Alternate Name (tooltip): {field.AlternateName}");
            }

            // Example: read default appearance (font, size, color) without modifying it
            DefaultAppearance appearance = acroForm.DefaultAppearance;
            if (appearance != null)
            {
                // Font is read‑only; use the constructor overload to create a new appearance if needed
                Console.WriteLine($"Default Appearance - Font Size: {appearance.FontSize}");
                // TextColor is the correct property name (not FontColor)
                Console.WriteLine($"Default Appearance - Text Color: {appearance.TextColor}");
            }

            // No need to save if only reviewing; uncomment the following lines to save changes
            // string outputPath = "reviewed_output.pdf";
            // doc.Save(outputPath);
        }
    }
}