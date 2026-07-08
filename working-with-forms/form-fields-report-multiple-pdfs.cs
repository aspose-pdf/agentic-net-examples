using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        foreach (var pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
            using (Document doc = new Document(pdfPath))
            {
                Console.WriteLine($"--- Form Fields Report for \"{pdfPath}\" ---");

                // Access the form object; it implements ICollection<WidgetAnnotation>
                Form form = doc.Form;

                int fieldNumber = 1;
                foreach (var annotation in form)
                {
                    // Each annotation is a WidgetAnnotation; cast to Field to access field properties
                    Field field = annotation as Field;
                    if (field == null)
                        continue;

                    // Field name (fallback to PartialName if Name is null)
                    string fieldName = field.Name ?? field.PartialName ?? "(unnamed)";

                    // Field type (runtime class name)
                    string fieldType = field.GetType().Name;

                    // Character limit: applicable for text fields (TextBoxField and its derivatives)
                    string charLimit = "N/A";
                    if (field is TextBoxField textBox)
                    {
                        // MaxLen == 0 means unlimited; otherwise report the limit
                        int maxLen = textBox.MaxLen;
                        charLimit = maxLen > 0 ? maxLen.ToString() : "Unlimited";
                    }

                    Console.WriteLine($"{fieldNumber}. Name: {fieldName}, Type: {fieldType}, CharLimit: {charLimit}");
                    fieldNumber++;
                }

                Console.WriteLine(); // Blank line between reports
            }
        }
    }
}