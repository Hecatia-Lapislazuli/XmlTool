using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;

namespace XmlTool {
	class Program {
		public const string header_comment =
			"This document was created with XmlTool, a tool originally developed for the Touhou Analepsia project by Urist McAurelian\n\n" +
			"Feel free to check out our own project at https://forums.taleworlds.com/index.php?threads/tbd-bannerlord-mod-touhou-analepsia-illusory-nirvana.411674/ \n" +
			"and join our discord with https://discord.gg/PUqYrGZ \n\n" +
            "If you want to use this tool yourself for you own project, you can find it at BLANK.";

		static void Main(string[] args) {
			Console.WriteLine("~~~NEW PROGRAM~~~");

			string root = AppDomain.CurrentDomain.BaseDirectory;
			string DSC = Path.DirectorySeparatorChar.ToString(); //For berevity's sake. 

			Directory.CreateDirectory(root + "Output");
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

			if (Directory.Exists(root + "Data" + DSC + "Settlements")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Settlements")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "Settlements");
					SettlementConverter.settlement_XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "Settlements" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

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
				if (Directory.Exists(root + "Data" + DSC + "SPCultures")) { //file.Split(DSC).Last().Split(".").First() + ".xml"
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "SPCultures")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "SPCultures" + DSC + "Languages");
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_" + file.Split(DSC).Last().Split(".").First() + "_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							CultureConverter.Cultures_CSVtoXML(file, root + "Output" + DSC + "SPCultures" + DSC + file.Split(DSC).Last().Split(".").First() + ".xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}

					}
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
				if (Directory.Exists(root + "Data" + DSC + "Settlements")) {
					string csv_file = null, xscene_file = null;

					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Settlements")) {
						switch(file.Split(".").Last()) {
							case "csv":
								if (csv_file == null) csv_file = file;
								else Console.WriteLine("Warning! There exists more than one csv in \"" + root + "Data" + DSC + "Settlements\", this is unsuported and unexpected behaviour may occur!");
								break;
							case "xscene":
								if (xscene_file == null) xscene_file = file;
								else Console.WriteLine("Warning! There exists more than one xscene in \"" + root + "Data" + DSC + "Settlements\", this is unsuported and unexpected behaviour may occur!");
								break;
							default:
								continue;
                        }
					}
					if (csv_file != null && xscene_file != null) {
						Directory.CreateDirectory(root + "Output" + DSC + "Settlements");

						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_settlements_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							SettlementConverter.settlement_CSVtoXML(csv_file, root + "Output" + DSC + "Settlements" + DSC + csv_file.Split(DSC).Last(), root + "Output" + DSC + "Settlements" + DSC + csv_file.Split(DSC).Last().Split(".").First() + ".xml", xscene_file, root + "Output" + DSC + "Settlements" + DSC + xscene_file.Split(DSC).Last().Split(".").First() + ".xscene", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					} else if (csv_file == null && xscene_file == null) {
					} else Console.WriteLine("You are missing either a xscene file and/or a csv file in \"" + root + "Data" + DSC + "Settlements\", to enable csv to xml conversion, please ensure both a xscene and csv file are present.");
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

		public static void writeHeadderComment(XmlWriter writter) {
			writter.WriteComment(header_comment);
        }
	}
}
