using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added to resolve WidgetAnnotation

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputFolder = "InputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Ensure the file exists before processing
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Open the PDF document inside a using block (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                Console.WriteLine($"--- {Path.GetFileName(pdfPath)} ---");

                // Iterate over all form fields (the Form collection implements ICollection<WidgetAnnotation>)
                foreach (WidgetAnnotation widget in doc.Form)
                {
                    // All items in the Form collection are Field instances
                    if (widget is Field field)
                    {
                        // Field name (fallback to PartialName if Name is null)
                        string fieldName = field.Name ?? field.PartialName ?? "(unnamed)";

                        // Concrete field type (e.g., TextBoxField, CheckBoxField, etc.)
                        string fieldType = field.GetType().Name;

                        // Character limit – only applicable to text‑based fields
                        string charLimit = "N/A";

                        // TextBoxField and its derived types expose the MaxLen property
                        if (field is TextBoxField txtField)
                        {
                            charLimit = txtField.MaxLen.ToString();
                        }
                        else if (field is FileSelectBoxField fileField)
                        {
                            charLimit = fileField.MaxLen.ToString();
                        }

                        // Output the information
                        Console.WriteLine($"Name: {fieldName}, Type: {fieldType}, Max Length: {charLimit}");
                    }
                }
            }
        }
    }
}
