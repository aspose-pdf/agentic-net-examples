using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm associated with the document
            Form form = doc.Form;

            // If the document has no form or no fields, report it
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Form field names:");

            // Enumerate the lowest‑level fields via the Fields property
            foreach (Field field in form.Fields)
            {
                // Prefer the fully qualified name; fall back to the simple name
                string fieldName = !string.IsNullOrEmpty(field.FullName) ? field.FullName : field.Name;
                Console.WriteLine($"- {fieldName}");
            }

            // Alternative enumeration using the form's enumerator (commented out)
            // var enumerator = form.GetEnumerator();
            // while (enumerator.MoveNext())
            // {
            //     var widget = enumerator.Current;
            //     Console.WriteLine(widget.FullName);
            // }
        }
    }
}