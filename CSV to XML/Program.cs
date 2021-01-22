using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;

namespace CSV_to_XML {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("~~~NEW PROGRAM~~~");

			//Console.WriteLine("What is your root directory?");
			string root = AppDomain.CurrentDomain.BaseDirectory;
			string DSC = Path.DirectorySeparatorChar.ToString(); //For berevity's sake. 
			//Console.WriteLine("Please input a address for the Settlement csv file: ");

			Directory.CreateDirectory(root+"Output");
			Directory.CreateDirectory(root + "Output" + DSC + "Languages");

			XmlWriterSettings localizationSettings = new XmlWriterSettings();
			localizationSettings.Indent = true;

			//TODO ClanConverter XML -> CSV converter

			if (Directory.Exists(root + "Data" + DSC + "SPCultures")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "SPCultures")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "SPCultures");
					CultureConverter.Cultures_XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "SPCultures" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			//TODO HeroConverter XML -> CSV converter
			//TODO KingdomConverter XML -> CSV converter

			if (Directory.Exists(root + "Data" + DSC + "NPCCharacters")) {
				foreach(string file in Directory.GetFiles(root + "Data" + DSC + "NPCCharacters")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "NPCCharacters");
                    NPCCharacterConverter.NPCCharacters_XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "NPCCharacters" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "partyTemplates")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "partyTemplates")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "partyTemplates");
					PartyTemplateConverter.PartyTemplates_XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "partyTemplates" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			//TODO better implement SettlementConverter.
			if (File.Exists(root + "Data" + DSC + "settlements.xml")) SettlementConverter.settlement_XMLtoCSV(root + "Data" + DSC + "settlements.xml", root + "Output" + DSC + "settlements.csv");

			using (XmlWriter module_stringsWriter = XmlWriter.Create(root + "Output" + DSC + "module_strings.xml", localizationSettings)) {
				module_stringsWriter.WriteStartElement("base");
				module_stringsWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
				module_stringsWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
				module_stringsWriter.WriteAttributeString("type", "string");
				module_stringsWriter.WriteStartElement("strings");


				//TODO
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spclans_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Clans.csv")) ClanConverter.Clans_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Clans.csv", root + "Output" + DSC + "spclans.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				//TODO
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spcultures_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Cultures.csv")) CultureConverter.Cultures_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Cultures.csv", root + "Output" + DSC + "spcultures.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				//TODO
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_heroes_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Heroes.csv")) HeroConverter.heroes_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Heroes.csv", root + "Output" + DSC + "heroes.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				//TODO
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spkingdoms_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Kingdoms.csv")) KingdomConverter.Kingdoms_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Kingdoms.csv", root + "Output" + DSC + "spkingdoms.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				if (Directory.Exists(root + "Data" + DSC + "NPCCharacters")) {
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "NPCCharacters")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "NPCCharacters" + DSC + "Languages");
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "NPCCharacters" + DSC + "Languages" + DSC + "std_" + file.Split(DSC).Last().Split(".").First() + "_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							NPCCharacterConverter.NPCCharacters_CSVtoXML(file, root + "Output" + DSC + "NPCCharacters" + DSC + file.Split(DSC).Last().Split(".").First() + ".xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					}
				}
				if (Directory.Exists(root + "Data" + DSC + "partyTemplates")) {
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "partyTemplates")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "partyTemplates");
						PartyTemplateConverter.PartyTemplates_CSVtoXML(file, root + "Output" + DSC + "partyTemplates" + DSC + file.Split(DSC).Last().Split(".").First() + ".xml");
					}
				}
				//TODO
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_settlements_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Settlements.csv")) SettlementConverter.settlement_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Settlements.csv", root + "Output" + DSC + "Touhou XML Data - Settlements.csv", root + "Output" + DSC + "settlements.xml", root + "Data" + DSC + "scene.xscene", root + "Output" + DSC + "scene.xscene", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}

				module_stringsWriter.WriteEndElement();
				module_stringsWriter.WriteEndElement();
			}
		}

		public static string Trim(string s) {
			if (s == null) return null;
			return s.Split(".").Last().Split("}").Last();
		}

		public static void writeLocalizationNode(XmlWriter writer, string id, string text) {
			writer.WriteStartElement("string");
			writer.WriteAttributeString("id", id);
			writer.WriteAttributeString("text", text);
			writer.WriteEndElement();
		}

		public static void initializeLocalizationWriter(XmlWriter localizationWriter) {
			localizationWriter.WriteStartElement("base");
			localizationWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			localizationWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			localizationWriter.WriteAttributeString("type", "string");
			localizationWriter.WriteStartElement("tags");
			localizationWriter.WriteStartElement("tag");
			localizationWriter.WriteAttributeString("language", "English");
			localizationWriter.WriteEndElement();
			localizationWriter.WriteEndElement();
			localizationWriter.WriteStartElement("strings");
		}
	}
}
