using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string optionToSelect = "Option2"; // the option name to select

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Locate the first RadioButtonField in the form collection
                RadioButtonField radio = null;
                foreach (Field field in doc.Form)
                {
                    if (field is RadioButtonField rb)
                    {
                        radio = rb;
                        break;
                    }
                }

                if (radio == null)
                {
                    Console.WriteLine("No radio button field found in the document.");
                }
                else
                {
                    // Find the index (1‑based) of the desired option name
                    int selectedIndex = -1;
                    for (int i = 0; i < radio.Options.Count; i++)
                    {
                        // Option objects expose a Name property (string)
                        if (string.Equals(radio.Options[i].Name, optionToSelect, StringComparison.OrdinalIgnoreCase))
                        {
                            selectedIndex = i + 1; // RadioButtonField.Selected uses 1‑based indexing
                            break;
                        }
                    }

                    if (selectedIndex > 0)
                    {
                        // Set the selected option
                        radio.Selected = selectedIndex;
                        Console.WriteLine($"Radio button set to option '{optionToSelect}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Option '{optionToSelect}' not found in the radio button field.");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPdf);
                Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
