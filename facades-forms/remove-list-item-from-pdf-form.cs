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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that the form contains the field named "Choices"
                if (doc.Form == null || !doc.Form.HasField("Choices"))
                {
                    Console.WriteLine("Form field 'Choices' not found.");
                }
                else
                {
                    // Retrieve the field and cast to ListBoxField
                    ListBoxField listBox = doc.Form["Choices"] as ListBoxField;

                    if (listBox != null)
                    {
                        // Check whether the option "Option B" exists (compare display name)
                        bool optionExists = false;
                        foreach (Option opt in listBox.Options)
                        {
                            if (opt.Name == "Option B")
                            {
                                optionExists = true;
                                break;
                            }
                        }

                        if (optionExists)
                        {
                            // Remove the option by its display name
                            listBox.DeleteOption("Option B");
                            Console.WriteLine("Removed 'Option B' from the 'Choices' list field.");
                        }
                        else
                        {
                            Console.WriteLine("'Option B' not found in the 'Choices' list field.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("'Choices' is not a ListBox field.");
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
