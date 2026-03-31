using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access the existing form associated with the document
            Form form = doc.Form;

            // Retrieve the field named "Comments"
            if (form["Comments"] is TextBoxField commentsField)
            {
                commentsField.Multiline = true;
                Console.WriteLine("Multiline property set for field 'Comments'.");
            }
            else
            {
                Console.Error.WriteLine("Field 'Comments' not found or is not a TextBoxField.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
