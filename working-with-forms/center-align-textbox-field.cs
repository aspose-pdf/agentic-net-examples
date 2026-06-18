using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;   // for HorizontalAlignment enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing a text field
        const string outputPdf = "output_centered.pdf";
        const string fieldName = "MyTextField";    // fully qualified name of the text field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // The Form object provides access to all AcroForm fields.
            Form form = doc.Form;

            // Locate the field by its full name. The Fields collection can be iterated,
            // or you can use the indexer if you know the exact name.
            Field field = null;
            foreach (Field f in form.Fields)
            {
                if (f.FullName.Equals(fieldName, StringComparison.Ordinal))
                {
                    field = f;
                    break;
                }
            }

            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Ensure the field is a text box (or any field that supports horizontal alignment).
            if (field is TextBoxField textBox)
            {
                // Set horizontal alignment to center.
                textBox.TextHorizontalAlignment = HorizontalAlignment.Center;

                // Optional: also set vertical alignment if desired.
                // textBox.TextVerticalAlignment = VerticalAlignment.Middle;

                // Save the modified document.
                doc.Save(outputPdf);
                Console.WriteLine($"Field alignment updated and saved to '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a text box field.");
            }
        }
    }
}