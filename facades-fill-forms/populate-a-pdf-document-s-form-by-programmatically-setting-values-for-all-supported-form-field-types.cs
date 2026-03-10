using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // source PDF with form fields
        const string outputPdf = "filled.pdf";     // destination PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade on the source PDF
        Form form = new Form(inputPdf);

        // Iterate through all form fields and fill them with sample data
        foreach (string fieldName in form.FieldNames)
        {
            // Determine the field type (e.g., Text, CheckBox, ComboBox, ListBox, Barcode, Image)
            FieldType fieldType = form.GetFieldType(fieldName);

            switch (fieldType)
            {
                // Textual fields (plain text)
                case FieldType.Text:
                    form.FillField(fieldName, "Sample text");
                    break;

                // Check box fields
                case FieldType.CheckBox:
                    form.FillField(fieldName, true); // check the box
                    break;

                // Combo box and list box fields – fill with the first option (index 0)
                case FieldType.ComboBox:
                case FieldType.ListBox:
                    form.FillField(fieldName, 0);
                    break;

                // Barcode fields – supply a string that the barcode can encode
                case FieldType.Barcode:
                    form.FillBarcodeField(fieldName, "1234567890");
                    break;

                // Image button fields – replace appearance with an image
                case FieldType.Image:
                    if (File.Exists("sample.png"))
                    {
                        using (FileStream imgStream = File.OpenRead("sample.png"))
                        {
                            form.FillImageField(fieldName, imgStream);
                        }
                    }
                    break;

                // Other field types are ignored in this generic example
                default:
                    break;
            }
        }

        // Save the filled PDF to the output path
        form.Save(outputPdf);
        Console.WriteLine($"Form fields populated and saved to '{outputPdf}'.");
    }
}
