using System;
using System.IO;
using System.Reflection;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // PDF files to process – use command‑line arguments or a default list
        string[] pdfFiles = args.Length > 0 ? args : new[] { "sample1.pdf", "sample2.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                Console.WriteLine($"--- Form fields in '{Path.GetFileName(pdfPath)}' ---");

                // Retrieve the AcroForm (if any) once and work with the local variable.
                var form = doc.Form;
                if (form == null || form.Fields == null || !form.Fields.Any())
                {
                    Console.WriteLine("No form fields found.");
                    continue;
                }

                // Iterate over all form fields
                foreach (Field field in form.Fields)
                {
                    string name = field.FullName;               // Field name
                    string type = field.GetType().Name;         // Field type (e.g., TextBoxField, CheckBoxField)

                    // Character limit applies mainly to text fields. Use reflection so the code compiles even if the
                    // MaxLength property is not present in the referenced Aspose.Pdf version.
                    string charLimit = "N/A";
                    if (field is TextBoxField txtField)
                    {
                        PropertyInfo maxLenProp = typeof(TextBoxField).GetProperty("MaxLength", BindingFlags.Public | BindingFlags.Instance);
                        if (maxLenProp != null)
                        {
                            int maxLen = (int)maxLenProp.GetValue(txtField);
                            charLimit = maxLen > 0 ? maxLen.ToString() : "Unlimited";
                        }
                        else
                        {
                            // Property not available – keep "N/A" or attempt a best‑effort default.
                            charLimit = "N/A";
                        }
                    }

                    Console.WriteLine($"Name: {name}, Type: {type}, CharLimit: {charLimit}");
                }
            }
        }
    }
}
