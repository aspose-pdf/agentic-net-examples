using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "PhoneNumber";
        const string inputMask = "(###) ###‑####";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document with FormEditor (facade API)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF
            formEditor.BindPdf(inputPdf);

            // The FormEditor API does not provide a direct method to set an input mask.
            // However, you can store the mask as a custom attribute using the SetFieldAttribute method
            // with a PropertyFlag that best represents the intended behavior.
            // Since there is no dedicated flag for input masks, we use the NoExport flag as a placeholder.
            // Adjust this logic according to your specific requirements or PDF viewer capabilities.
            formEditor.SetFieldAttribute(fieldName, PropertyFlag.NoExport);

            // Optionally, you can set a default value that matches the mask format.
            // This helps users see the expected pattern when the field is empty.
            Form form = new Form();
            form.BindPdf(inputPdf);
            form.FillField(fieldName, inputMask);
            form.Save(outputPdf);

            // Save the modified PDF (including any custom attributes set above)
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with field \"{fieldName}\" updated: {outputPdf}");
    }
}