using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_template.pdf";
        const string outputPath = "filled_encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Fill form fields (example field names)
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // Text box field
                    var nameField = doc.Form["Name"];
                    if (nameField is TextBoxField txtBox)
                    {
                        txtBox.Value = "John Doe";
                    }

                    // Checkbox field – use runtime type inspection to avoid compile‑time binding issues
                    var agreeField = doc.Form["Agree"];
                    if (agreeField != null && agreeField.GetType().Name == "CheckboxField")
                    {
                        dynamic chk = agreeField; // CheckboxField has a bool Checked property
                        chk.Checked = true;
                    }

                    // Radio button group – also handled via reflection/dynamic
                    var genderField = doc.Form["Gender"];
                    if (genderField != null && genderField.GetType().Name == "RadioButtonOptionField")
                    {
                        dynamic radio = genderField; // RadioButtonOptionField exposes Selected property
                        radio.Selected = "Male";
                    }

                    // Combo box field
                    var countryField = doc.Form["Country"];
                    if (countryField is ComboBoxField combo)
                    {
                        combo.Value = "USA";
                    }
                }

                // Apply encryption with AES‑256 and basic permissions
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt("user123", "owner123", perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form filled and encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
