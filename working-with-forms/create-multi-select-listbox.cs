using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form
        using (Document doc = new Document(inputPath))
        {
            // Verify that the form and the specific list box field exist
            if (doc.Form != null && doc.Form.HasField("myListBox"))
            {
                // Cast the field to ListBoxField
                ListBoxField listBox = doc.Form["myListBox"] as ListBoxField;
                if (listBox != null)
                {
                    // Enable multi‑selection
                    listBox.MultiSelect = true;

                    // Determine the 1‑based indices of the options we want to select
                    var wantedValues = new HashSet<string> { "OptionA", "OptionC", "OptionD" };
                    var selectedIndices = new List<int>();
                    foreach (Option opt in listBox.Options)
                    {
                        // Match either the display name (Name) or the export value (Value)
                        if (wantedValues.Contains(opt.Name) || wantedValues.Contains(opt.Value))
                        {
                            selectedIndices.Add(opt.Index); // Index is 1‑based
                        }
                    }

                    // Assign the selected indices (int[]). No Clear() method exists.
                    listBox.SelectedItems = selectedIndices.ToArray();
                }
                else
                {
                    Console.Error.WriteLine("Field 'myListBox' is not a ListBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine("List box field 'myListBox' not found.");
            }

            // Save the updated PDF with the selections applied
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi-select list box populated and saved to '{outputPath}'.");
    }
}
