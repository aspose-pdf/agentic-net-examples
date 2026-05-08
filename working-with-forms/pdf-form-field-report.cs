using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Expect PDF file paths as command‑line arguments.
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide one or more PDF file paths as arguments.");
            return;
        }

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load each PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                Console.WriteLine($"--- Form Field Report for \"{Path.GetFileName(pdfPath)}\" ---");

                // Iterate over all form fields. The Form collection yields Field objects directly.
                foreach (Field field in doc.Form)
                {
                    string fieldName = field.FullName;               // Fully qualified field name.
                    string fieldType = field.GetType().Name;         // Concrete field class name.

                    // Character limit applies to text‑based fields (TextBoxField and its derivatives).
                    string charLimit = "N/A";
                    if (field is TextBoxField txtField)
                    {
                        // MaxLen == 0 means no explicit limit.
                        charLimit = txtField.MaxLen > 0 ? txtField.MaxLen.ToString() : "Unlimited";
                    }

                    Console.WriteLine($"Name: {fieldName}, Type: {fieldType}, CharLimit: {charLimit}");
                }

                Console.WriteLine(); // Blank line between reports.
            }
        }
    }
}