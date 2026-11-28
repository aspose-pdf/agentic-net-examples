using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "contract_form.pdf";
        const string outputPath = "readonly_id.pdf";
        const string fieldName = "ContractID";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);
            // Set the field to read‑only using the correct FormEditor API
            formEditor.SetFieldAttribute(fieldName, PropertyFlag.ReadOnly);
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Saved read‑only PDF to '{outputPath}'.");
    }
}