using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF.
        // The Form class implements IDisposable via SaveableFacade, so we wrap it in a using block.
        using (Form form = new Form(inputPdf))
        {
            // Retrieve all field names present in the PDF form.
            string[] fieldNames = form.FieldNames;

            if (fieldNames == null || fieldNames.Length == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each field, obtain its value, and display it.
            foreach (string fieldName in fieldNames)
            {
                // GetField returns the current value of the field as an object.
                // It may be null for empty fields, so we handle that gracefully.
                object fieldValue = form.GetField(fieldName);
                string valueText = fieldValue?.ToString() ?? "<empty>";

                Console.WriteLine($"Field: {fieldName}  Value: {valueText}");
            }
        }
    }
}