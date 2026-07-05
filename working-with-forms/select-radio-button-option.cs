using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Usage: RadioSelect.exe <inputPdf> <outputPdf> <fieldName> <optionValue>
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <fieldName> <optionValue>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string fieldName  = args[2];   // Partial or full name of the radio button field
        string optionValue = args[3]; // The option to select

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Locate the radio button field by name
                RadioButtonField? radioField = null;
                foreach (Field f in doc.Form)
                {
                    if (f is RadioButtonField rb &&
                        (rb.PartialName.Equals(fieldName, StringComparison.OrdinalIgnoreCase) ||
                         rb.FullName.Equals(fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        radioField = rb;
                        break;
                    }
                }

                if (radioField == null)
                {
                    Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                }
                else
                {
                    // Find the index of the desired option (1‑based)
                    int optionIndex = -1;
                    for (int i = 0; i < radioField.Options.Count; i++)
                    {
                        // Option is an object; compare its Name (or ExportValue) with the supplied value
                        if (string.Equals(radioField.Options[i].Name, optionValue, StringComparison.OrdinalIgnoreCase))
                        {
                            optionIndex = i + 1; // Selected property is 1‑based
                            break;
                        }
                    }

                    if (optionIndex == -1)
                    {
                        Console.Error.WriteLine($"Option '{optionValue}' not found in field '{fieldName}'.");
                    }
                    else
                    {
                        // Select the option
                        radioField.Selected = optionIndex;
                        // Optional: allow deselection if needed
                        // radioField.NoToggleToOff = false;
                        Console.WriteLine($"Selected option '{optionValue}' (index {optionIndex}) for field '{fieldName}'.");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
