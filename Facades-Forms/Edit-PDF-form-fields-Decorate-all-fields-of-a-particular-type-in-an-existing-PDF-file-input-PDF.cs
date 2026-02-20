using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // args: <inputPdf> <outputPdf> <fieldType>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <fieldType>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fieldTypeName = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to FormEditor
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Convert the field type name to the enum value
                if (!Enum.TryParse<FieldType>(fieldTypeName, true, out FieldType fieldType))
                {
                    Console.Error.WriteLine($"Invalid field type: {fieldTypeName}");
                    return;
                }

                // Decorate all fields of the specified type
                formEditor.DecorateField(fieldType);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}