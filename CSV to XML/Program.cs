using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using CsvHelper;

namespace CSV_to_XML {
	class Program {
		static void Main(string[] args) {
			//Console.WriteLine("What is your root directory?");
			string root = AppDomain.CurrentDomain.BaseDirectory;
			//Console.WriteLine("Please input a address for the Settlement csv file: ");

			Directory.CreateDirectory(root+"Output");

			//string root = "O:\\Games\\SteamLibrary\\steamapps\\common\\Mount & Blade II Bannerlord\\Modules\\TouhouAnalepsia\\";
			if (File.Exists(root + "Data\\Touhou XML Data - Settlements.csv")) settlement_CSVtoXML(root + "Data\\Touhou XML Data - Settlements.csv", root + "Output\\settlements.xml");

			if (File.Exists(root + "Data\\Touhou XML Data - Heroes.csv")) heroes_CSVtoXML(root + "Data\\Touhou XML Data - Heroes.csv", root + "Output\\heroes.xml");
			if (File.Exists(root + "Data\\Touhou XML Data - NPCCharacters.csv")) NPCCharacters_CSVtoXML(root + "Data\\Touhou XML Data - NPCCharacters.csv", root + "Output\\lords.xml");
			if (File.Exists(root + "Data\\Touhou XML Data - NPCCharacters - Companions.csv")) NPCCharacters_CSVtoXML(root + "Data\\Touhou XML Data - NPCCharacters - Companions.csv", root + "Output\\companions.xml");
			if (File.Exists(root + "Data\\Touhou XML Data - NPCCharacters - Units.csv")) NPCCharacters_CSVtoXML(root + "Data\\Touhou XML Data - NPCCharacters - Units.csv", root + "Output\\spspecialcharacters.xml");

			if (File.Exists(root + "Data\\Touhou XML Data - Kingdoms.csv")) Kingdoms_CSVtoXML(root + "Data\\Touhou XML Data - Kingdoms.csv", root + "Output\\spkingdoms.xml");
			if (File.Exists(root + "Data\\Touhou XML Data - Clans.csv")) Clans_CSVtoXML(root + "Data\\Touhou XML Data - Clans.csv", root + "Output\\spclans.xml");

			if (File.Exists(root + "Data\\Touhou XML Data - Cultures.csv")) Cultures_CSVtoXML(root + "Data\\Touhou XML Data - Cultures.csv", root + "Output\\spcultures.xml");
		}
		public static void settlement_CSVtoXML(string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			SettlementRecord settlementRecord = new SettlementRecord();
			IEnumerable<SettlementRecord> records = csv.EnumerateRecords(settlementRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Settlements");
				foreach (SettlementRecord record in records) {
					writer.WriteStartElement("Settlement");

					//Changes
					record.Comp_Town_is_castle = record.Comp_Town_is_castle.ToLower();

					//Defaults

					//Temporary


					//Write
					writer.WriteAttributeString(null, "id", null, record.id);
					writer.WriteAttributeString(null, "name", null, "{=Settlements.Settlement.name." + record.id + "}" + record.name);
					if (!record.owner.Equals("")) writer.WriteAttributeString(null, "owner", null, record.owner);
					writer.WriteAttributeString(null, "posX", null, record.posX);
					writer.WriteAttributeString(null, "posY", null, record.posY);
					if (!record.prosperity.Equals("")) writer.WriteAttributeString(null, "prosperity", null, record.prosperity);
					if (!record.culture.Equals("")) writer.WriteAttributeString(null, "culture", null, record.culture);
					if (!record.gate_posX.Equals("")) writer.WriteAttributeString(null, "gate_posX", null, record.gate_posX);
					if (!record.gate_posX.Equals("")) writer.WriteAttributeString(null, "gate_posY", null, record.gate_posY);
					if (!record.gate_rotation.Equals("")) writer.WriteAttributeString(null, "gate_rotation", null, record.gate_rotation);
					if (!record.type.Equals("")) writer.WriteAttributeString(null, "type", null, record.type);
					if (!record.text.Equals("")) writer.WriteAttributeString(null, "text", null, record.text);
					if (!record.Comp_Town_is_castle.Equals("") || !record.Comp_Village_village_type.Equals("") || !record.Comp_Hideout_map_icon.Equals("")) {
						writer.WriteStartElement("Component");

						if (!record.Comp_Town_is_castle.Equals("")) { // Town
							writer.WriteStartElement("Town");

							//Set up defaults
							if (record.Comp_Town_level.Equals("")) record.Comp_Town_level = "1";
							if (record.Comp_Town_background_crop_position.Equals("")) record.Comp_Town_background_crop_position = "0.0";
							if (record.Comp_Town_background_mesh.Equals("")) record.Comp_Town_background_mesh = "menu_empire_1";
							if (record.Comp_Town_wait_mesh.Equals("")) record.Comp_Town_wait_mesh = "wait_empire_town";
							if (record.Comp_Town_gate_rotation.Equals("")) record.Comp_Town_gate_rotation = "0.908";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id.Replace("_", "_comp_"));
							writer.WriteAttributeString(null, "is_castle", null, record.Comp_Town_is_castle);
							writer.WriteAttributeString(null, "level", null, record.Comp_Town_level);
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Town_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Town_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Town_wait_mesh);
							writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Town_gate_rotation);

							writer.WriteEndElement();
						}

						if (!record.Comp_Village_village_type.Equals("")) { // Village
							writer.WriteStartElement("Village");

							//Set up defaults
							if (record.Comp_Village_hearth.Equals("")) record.Comp_Village_hearth = "200"; // Seeded Random?
							if (record.Comp_Village_background_crop_position.Equals("")) record.Comp_Village_background_crop_position = "0.0";
							if (record.Comp_Village_background_mesh.Equals("")) record.Comp_Village_background_mesh = "gui_bg_village_empire";
							if (record.Comp_Village_castle_background_mesh.Equals("")) record.Comp_Village_castle_background_mesh = "gui_bg_castle_empire";
							if (record.Comp_Village_wait_mesh.Equals("")) record.Comp_Village_wait_mesh = "wait_empire_village";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id.Replace("village_", "village_comp_"));
							writer.WriteAttributeString(null, "village_type", null, "VillageType." + record.Comp_Village_village_type);
							if (!record.Comp_Village_gate_rotation.Equals("")) writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Village_gate_rotation);
							writer.WriteAttributeString(null, "hearth", null, record.Comp_Village_hearth);
							if (record.id.Contains("castle_village_")) {
								writer.WriteAttributeString(null, "trade_bound", null, record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
								writer.WriteAttributeString(null, "bound", null, record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
							} else {
								writer.WriteAttributeString(null, "trade_bound", null, record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
								writer.WriteAttributeString(null, "bound", null, record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
							}
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Village_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Village_background_mesh);
							writer.WriteAttributeString(null, "castle_background_mesh", null, record.Comp_Village_castle_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Village_wait_mesh);

							writer.WriteEndElement();
						}

						if (!record.Comp_Hideout_map_icon.Equals("")) { // Hideout
							writer.WriteStartElement("Hideout");

							//Set up defaults
							if (record.Comp_Hideout_scene_name.Equals("")) record.Comp_Hideout_scene_name = "bandit_forest";
							if (record.Comp_Hideout_background_crop_position.Equals("")) record.Comp_Hideout_background_crop_position = "0.0";
							if (record.Comp_Hideout_background_mesh.Equals("")) record.Comp_Hideout_background_mesh = "empire_twn_scene_bg";
							if (record.Comp_Hideout_wait_mesh.Equals("")) record.Comp_Hideout_wait_mesh = "wait_hideout_forest";
							if (record.Comp_Hideout_gate_rotation.Equals("")) record.Comp_Hideout_gate_rotation = "0.0";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id);
							writer.WriteAttributeString(null, "map_icon", null, record.Comp_Hideout_map_icon);
							writer.WriteAttributeString(null, "scene_name", null, record.Comp_Hideout_scene_name);
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Town_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Hideout_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Town_wait_mesh);
							writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Hideout_gate_rotation);

							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.Locations_complex_template.Equals("")) {
						writer.WriteStartElement("Locations");
						writer.WriteAttributeString(null, "complex_template", null, record.Locations_complex_template);
						if (!record.Locations_Location0_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location0_id);
							if (!record.Locations_Location0_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location0_id);
							if (!record.Locations_Location0_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location0_scene_name_1);
							if (!record.Locations_Location0_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location0_scene_name_2);
							if (!record.Locations_Location0_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location0_scene_name_3);
							if (!record.Locations_Location0_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location0_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location1_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location1_id);
							if (!record.Locations_Location1_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location1_id);
							if (!record.Locations_Location1_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location1_scene_name_1);
							if (!record.Locations_Location1_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location1_scene_name_2);
							if (!record.Locations_Location1_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location1_scene_name_3);
							if (!record.Locations_Location1_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location1_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location2_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location2_id);
							if (!record.Locations_Location2_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location2_id);
							if (!record.Locations_Location2_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location2_scene_name_1);
							if (!record.Locations_Location2_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location2_scene_name_2);
							if (!record.Locations_Location2_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location2_scene_name_3);
							if (!record.Locations_Location2_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location2_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location3_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location3_id);
							if (!record.Locations_Location3_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location3_id);
							if (!record.Locations_Location3_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location3_scene_name_1);
							if (!record.Locations_Location3_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location3_scene_name_2);
							if (!record.Locations_Location3_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location3_scene_name_3);
							if (!record.Locations_Location3_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location3_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location4_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location4_id);
							if (!record.Locations_Location4_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location4_id);
							if (!record.Locations_Location4_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location4_scene_name_1);
							if (!record.Locations_Location4_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location4_scene_name_2);
							if (!record.Locations_Location4_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location4_scene_name_3);
							if (!record.Locations_Location4_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location4_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location5_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location5_id);
							if (!record.Locations_Location5_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location5_id);
							if (!record.Locations_Location5_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location5_scene_name_1);
							if (!record.Locations_Location5_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location5_scene_name_2);
							if (!record.Locations_Location5_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location5_scene_name_3);
							if (!record.Locations_Location5_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location5_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location6_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location6_id);
							if (!record.Locations_Location6_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location6_id);
							if (!record.Locations_Location6_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location6_scene_name_1);
							if (!record.Locations_Location6_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location6_scene_name_2);
							if (!record.Locations_Location6_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location6_scene_name_3);
							if (!record.Locations_Location6_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location6_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location7_id.Equals("")) {
							writer.WriteStartElement("Locations");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location7_id);
							if (!record.Locations_Location7_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location7_id);
							if (!record.Locations_Location7_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location7_scene_name_1);
							if (!record.Locations_Location7_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location7_scene_name_2);
							if (!record.Locations_Location7_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location7_scene_name_3);
							if (!record.Locations_Location7_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location7_max_prosperity);

							writer.WriteEndElement();
						}
						writer.WriteEndElement();

					}

					bool automatic_common_areas_village = true;

					if (!record.CommonAreas_Area0_type.Equals("") || !record.CommonAreas_Area1_type.Equals("") || !record.CommonAreas_Area2_type.Equals("") || (!record.Comp_Village_village_type.Equals("") && automatic_common_areas_village)) {
						writer.WriteStartElement("CommonAreas");

						if (!record.Comp_Village_village_type.Equals("") && automatic_common_areas_village) {
							if (record.CommonAreas_Area0_type.Equals("")) {
								record.CommonAreas_Area0_type = "Pasture";
								record.CommonAreas_Area0_name = "{=fOUsLdZR}Pasture";
							}
							if (record.CommonAreas_Area1_type.Equals("")) {
								record.CommonAreas_Area1_type = "Thicket";
								record.CommonAreas_Area1_name = "{=66Mzk0NZ}Thicket";
							}
							if (record.CommonAreas_Area2_type.Equals("")) {
								record.CommonAreas_Area2_type = "Bog";
								record.CommonAreas_Area2_name = "{=iXA5SttU}Bog";
							}
						}

						if (!record.CommonAreas_Area0_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area0_type);
							writer.WriteAttributeString(null, "name", null, record.CommonAreas_Area0_name);
							writer.WriteEndElement();
						}
						if (!record.CommonAreas_Area1_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area1_type);
							writer.WriteAttributeString(null, "name", null, record.CommonAreas_Area1_name);
							writer.WriteEndElement();
						}
						if (!record.CommonAreas_Area2_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area2_type);
							writer.WriteAttributeString(null, "name", null, record.CommonAreas_Area2_name);
							writer.WriteEndElement();
						}
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.Flush();
			}
		}
		public class SettlementRecord {
			//Basic
			public string id { get; set; }
			public string name { get; set; }
			public string owner { get; set; }
			public string posX { get; set; }
			public string posY { get; set; }
			public string prosperity { get; set; }
			public string culture { get; set; }
			public string gate_posX { get; set; }
			public string gate_posY { get; set; }
			public string gate_rotation { get; set; }
			public string type { get; set; }
			public string text { get; set; }

			//Components
			//Town
			//public string Comp_Town_id { get; set; } //Handle this dynamically
			public string Comp_Town_is_castle { get; set; }
			public string Comp_Town_level { get; set; }
			public string Comp_Town_background_crop_position { get; set; }
			public string Comp_Town_background_mesh { get; set; }
			public string Comp_Town_wait_mesh { get; set; }
			public string Comp_Town_gate_rotation { get; set; }
			//Village
			//public string Comp_village_id { get; set; }
			public string Comp_Village_village_type { get; set; }
			public string Comp_Village_gate_rotation { get; set; }
			public string Comp_Village_hearth { get; set; }
			public string Comp_Village_trade_bound { get; set; }
			public string Comp_Village_bound { get; set; }
			public string Comp_Village_background_crop_position { get; set; }
			public string Comp_Village_background_mesh { get; set; }
			public string Comp_Village_castle_background_mesh { get; set; }
			public string Comp_Village_wait_mesh { get; set; }
			//Hideout
			public string Comp_Hideout_id { get; set; }
			public string Comp_Hideout_map_icon { get; set; }
			public string Comp_Hideout_scene_name { get; set; }
			public string Comp_Hideout_background_crop_position { get; set; }
			public string Comp_Hideout_background_mesh { get; set; }
			public string Comp_Hideout_wait_mesh { get; set; }
			public string Comp_Hideout_gate_rotation { get; set; }

			//Locations
			public string Locations_complex_template { get; set; }
			//Location 0
			public string Locations_Location0_id { get; set; }
			public string Locations_Location0_scene_name { get; set; }
			public string Locations_Location0_scene_name_1 { get; set; }
			public string Locations_Location0_scene_name_2 { get; set; }
			public string Locations_Location0_scene_name_3 { get; set; }
			public string Locations_Location0_max_prosperity { get; set; }
			//Location 1
			public string Locations_Location1_id { get; set; }
			public string Locations_Location1_scene_name { get; set; }
			public string Locations_Location1_scene_name_1 { get; set; }
			public string Locations_Location1_scene_name_2 { get; set; }
			public string Locations_Location1_scene_name_3 { get; set; }
			public string Locations_Location1_max_prosperity { get; set; }
			//Location 2
			public string Locations_Location2_id { get; set; }
			public string Locations_Location2_scene_name { get; set; }
			public string Locations_Location2_scene_name_1 { get; set; }
			public string Locations_Location2_scene_name_2 { get; set; }
			public string Locations_Location2_scene_name_3 { get; set; }
			public string Locations_Location2_max_prosperity { get; set; }
			//Location 3
			public string Locations_Location3_id { get; set; }
			public string Locations_Location3_scene_name { get; set; }
			public string Locations_Location3_scene_name_1 { get; set; }
			public string Locations_Location3_scene_name_2 { get; set; }
			public string Locations_Location3_scene_name_3 { get; set; }
			public string Locations_Location3_max_prosperity { get; set; }
			//Location 4
			public string Locations_Location4_id { get; set; }
			public string Locations_Location4_scene_name { get; set; }
			public string Locations_Location4_scene_name_1 { get; set; }
			public string Locations_Location4_scene_name_2 { get; set; }
			public string Locations_Location4_scene_name_3 { get; set; }
			public string Locations_Location4_max_prosperity { get; set; }
			//Location 5
			public string Locations_Location5_id { get; set; }
			public string Locations_Location5_scene_name { get; set; }
			public string Locations_Location5_scene_name_1 { get; set; }
			public string Locations_Location5_scene_name_2 { get; set; }
			public string Locations_Location5_scene_name_3 { get; set; }
			public string Locations_Location5_max_prosperity { get; set; }
			//Location 6
			public string Locations_Location6_id { get; set; }
			public string Locations_Location6_scene_name { get; set; }
			public string Locations_Location6_scene_name_1 { get; set; }
			public string Locations_Location6_scene_name_2 { get; set; }
			public string Locations_Location6_scene_name_3 { get; set; }
			public string Locations_Location6_max_prosperity { get; set; }
			//Location 7
			public string Locations_Location7_id { get; set; }
			public string Locations_Location7_scene_name { get; set; }
			public string Locations_Location7_scene_name_1 { get; set; }
			public string Locations_Location7_scene_name_2 { get; set; }
			public string Locations_Location7_scene_name_3 { get; set; }
			public string Locations_Location7_max_prosperity { get; set; }
			//CommonAreas
			//Area 0
			public string CommonAreas_Area0_type { get; set; }
			public string CommonAreas_Area0_name { get; set; }
			//Area 1
			public string CommonAreas_Area1_type { get; set; }
			public string CommonAreas_Area1_name { get; set; }
			//Area 2
			public string CommonAreas_Area2_type { get; set; }
			public string CommonAreas_Area2_name { get; set; }
		}

		public static void heroes_CSVtoXML(string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			HeroRecord record = new HeroRecord();
			IEnumerable<HeroRecord> records = csv.EnumerateRecords(record);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Heroes");
				foreach (HeroRecord heroRecord in records) {
					writer.WriteStartElement("Hero");

					//Changes
					record.is_noble = record.is_noble.ToLower();
					record.alive = record.alive.ToLower();

					//Temporary

					//Write
					writer.WriteAttributeString(null, "id", null, heroRecord.id);
					if (!heroRecord.is_noble.Equals("")) writer.WriteAttributeString(null, "is_noble", null, heroRecord.is_noble);
					if (!heroRecord.father.Equals("")) writer.WriteAttributeString(null, "father", null, heroRecord.father);
					if (!heroRecord.mother.Equals("")) writer.WriteAttributeString(null, "mother", null, heroRecord.mother);
					writer.WriteAttributeString(null, "faction", null, "Faction." + heroRecord.faction);
					if (!heroRecord.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, heroRecord.banner_key);
					if (!heroRecord.spouse.Equals("")) writer.WriteAttributeString(null, "spouse", null, heroRecord.spouse);
					if (!heroRecord.alive.Equals("")) writer.WriteAttributeString(null, "alive", null, heroRecord.alive);
					if (!heroRecord.text.Equals("")) writer.WriteAttributeString(null, "text", null, heroRecord.text);

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class HeroRecord {
			public string id { get; set; }
			public string is_noble { get; set; }
			public string father { get; set; }
			public string mother { get; set; }
			public string faction { get; set; }
			public string banner_key { get; set; }
			public string spouse { get; set; }
			public string alive { get; set; }
			public string text { get; set; }

		}

		public static void NPCCharacters_CSVtoXML(string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			NPCCharacterRecord npcCharacterRecord = new NPCCharacterRecord();
			IEnumerable<NPCCharacterRecord> records = csv.EnumerateRecords(npcCharacterRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("NPCCharacters");
				foreach (NPCCharacterRecord record in records) {
					writer.WriteStartElement("NPCCharacter");

					//Changes
					record.is_basic_troop = record.is_basic_troop.ToLower();
					record.is_child_template = record.is_child_template.ToLower();
					record.is_companion = record.is_companion.ToLower();
					record.is_female = record.is_female.ToLower();
					record.is_hero = record.is_hero.ToLower();
					record.is_mercenary = record.is_mercenary.ToLower();
					record.is_template = record.is_template.ToLower();

					//Defaults
					if (record.default_group.Equals("")) record.default_group = "Infantry";
					if (record.civilianTemplate.Equals("")) record.civilianTemplate = "npc_armed_wanderer_equipment_template_empire";
					if (record.battleTemplate.Equals("")) record.battleTemplate = "emp_bat_template_lady";
					if (record.level.Equals("")) record.level = "15";

					//Temporary
					if (record.age.Equals("")) record.age = "23";

					//Write
					writer.WriteAttributeString(null, "id", null, record.id);
					writer.WriteAttributeString(null, "default_group", null, record.default_group);
					writer.WriteAttributeString(null, "level", null, record.level);
					if (!record.age.Equals("")) writer.WriteAttributeString(null, "age", null, record.age);////
					if (!record.voice.Equals("")) writer.WriteAttributeString(null, "voice", null, record.voice);//
					writer.WriteAttributeString(null, "is_hero", null, record.is_hero);
					if (!record.is_female.Equals("")) writer.WriteAttributeString(null, "is_female", null, record.is_female);////
					if (!record.is_basic_troop.Equals("")) writer.WriteAttributeString(null, "is_basic_troop", null, record.is_basic_troop);//
					if (!record.is_template.Equals("")) writer.WriteAttributeString(null, "is_template", null, record.is_template);//
					if (!record.is_child_template.Equals("")) writer.WriteAttributeString(null, "is_child_template", null, record.is_child_template);//
					writer.WriteAttributeString(null, "culture", null, "Culture." + record.culture);
					writer.WriteAttributeString(null, "name", null, "{=NPCCharacter."+record.id+".name}"+record.name);
					if (!record.banner_symbol_mesh_name.Equals("")) writer.WriteAttributeString(null, "banner_symbol_mesh_name", null, record.banner_symbol_mesh_name);////
					if (!record.banner_symbol_color.Equals("")) writer.WriteAttributeString(null, "banner_symbol_color", null, record.banner_symbol_color);//
					if (!record.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, record.banner_key);////
					if (!record.occupation.Equals("")) writer.WriteAttributeString(null, "occupation", null, record.occupation);////
					if (!record.civilianTemplate.Equals("")) writer.WriteAttributeString(null, "civilianTemplate", null, "NPCCharacter." + record.civilianTemplate);//
					if (!record.battleTemplate.Equals("")) writer.WriteAttributeString(null, "battleTemplate", null, "NPCCharacter." + record.battleTemplate);//
					if (!record.is_companion.Equals("")) writer.WriteAttributeString(null, "is_companion", null, record.is_companion);////
					if (!record.offset.Equals("")) writer.WriteAttributeString(null, "offset", null, record.offset);////
					if (!record.is_mercenary.Equals("")) writer.WriteAttributeString(null, "is_mercenary", null, record.is_mercenary);////
					if (!record.formation_position_preference.Equals("")) writer.WriteAttributeString(null, "formation_position_preference", null, record.formation_position_preference);////
					if (!record.default_equipment_set.Equals("")) writer.WriteAttributeString(null, "default_equipment_set", null, record.default_equipment_set);//
					if (!record.skill_template.Equals("")) writer.WriteAttributeString(null, "skill_template", null, "NPCCharacter."+record.skill_template);//
					if (!record.upgrade_requires.Equals("")) writer.WriteAttributeString(null, "upgrade_requires", null, record.upgrade_requires);//

					//Face
					if(true) {
						writer.WriteStartElement("face");

						if(!record.Face_face_key_value.Equals("")) {
							writer.WriteStartElement("face_key");

							writer.WriteAttributeString("value",record.Face_face_key_value);
							if(!record.Face_face_key_max_value.Equals("")) writer.WriteAttributeString("max_value", record.Face_face_key_max_value);

							writer.WriteEndElement();
						}

						if (!record.Face_face_key_template_value.Equals("")) {
							writer.WriteStartElement("face_key_template");
							writer.WriteAttributeString("value", "NPCCharacter." + record.Face_face_key_template_value);
							writer.WriteEndElement();
						}

						if(!record.Face_BodyProperties_version.Equals("")) {
							writer.WriteStartElement("BodyProperties");

							writer.WriteAttributeString("version", record.Face_BodyProperties_version);
							writer.WriteAttributeString("key", record.Face_BodyProperties_key);
							if (!record.Face_BodyProperties_age.Equals("")) writer.WriteAttributeString("age", record.Face_BodyProperties_age);
							if (!record.Face_BodyProperties_weight.Equals("")) writer.WriteAttributeString("weight", record.Face_BodyProperties_weight);
							if (!record.Face_BodyProperties_build.Equals("")) writer.WriteAttributeString("build", record.Face_BodyProperties_build);

							writer.WriteEndElement();

							if(!record.Face_BodyPropertiesMax_version.Equals("")) {
								writer.WriteStartElement("BodyPropertiesMax");

								writer.WriteAttributeString("version", record.Face_BodyPropertiesMax_version);
								writer.WriteAttributeString("key", record.Face_BodyPropertiesMax_key);
								if (!record.Face_BodyPropertiesMax_age.Equals("")) writer.WriteAttributeString("age", record.Face_BodyPropertiesMax_age);
								if (!record.Face_BodyPropertiesMax_weight.Equals("")) writer.WriteAttributeString("weight", record.Face_BodyPropertiesMax_weight);
								if (!record.Face_BodyPropertiesMax_build.Equals("")) writer.WriteAttributeString("build", record.Face_BodyPropertiesMax_build);

								writer.WriteEndElement();
							}
						}

						if(!record.Face_hair_tags_hair_tag0_name.Equals("") || !record.Face_hair_tags_hair_tag1_name.Equals("") || !record.Face_hair_tags_hair_tag2_name.Equals("")) {
							writer.WriteStartElement("hair_tags");

							if(!record.Face_hair_tags_hair_tag0_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_hair_tags_hair_tag1_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_hair_tags_hair_tag2_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}
						if (!record.Face_beard_tags_beard_tag0_name.Equals("") || !record.Face_beard_tags_beard_tag1_name.Equals("") || !record.Face_beard_tags_beard_tag2_name.Equals("")) {
							writer.WriteStartElement("beard_tags");

							if (!record.Face_beard_tags_beard_tag0_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_beard_tags_beard_tag1_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_beard_tags_beard_tag2_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}
						if (!record.Face_tattoo_tags_tattoo_tag0_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag1_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
							writer.WriteStartElement("tattoo_tags");

							if (!record.Face_tattoo_tags_tattoo_tag0_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_tattoo_tags_tattoo_tag1_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					//Companion
					if (record.is_companion.Equals("true", StringComparison.OrdinalIgnoreCase)) {
						writer.WriteStartElement("Components");
						writer.WriteStartElement("Companion");
						writer.WriteAttributeString(null, "id", null, record.id.Replace("companion_", "companion_comp_"));
						writer.WriteEndElement();
						writer.WriteEndElement();
					}

					//Equipment Set - Is this actually needed?
					writer.WriteStartElement("equipmentSet");
					writer.WriteEndElement();

					if (!record.upgrade_targets_upgrade_target0_id.Equals("") || !record.upgrade_targets_upgrade_target1_id.Equals("") ||
						!record.upgrade_targets_upgrade_target2_id.Equals("") || !record.upgrade_targets_upgrade_target3_id.Equals("")) {
						writer.WriteStartElement("upgrade_targets");

						if (!record.upgrade_targets_upgrade_target0_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target0_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target1_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target1_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target2_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target2_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target3_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target3_id);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		} //Unfinished
		public class NPCCharacterRecord {
			public string id { get; set; }
			public string default_group { get; set; }
			public string voice { get; set; }
			public string is_hero { get; set; }
			public string is_female { get; set; }
			public string is_basic_troop { get; set; }
			public string is_template { get; set; }
			public string is_child_template { get; set; }
			public string culture { get; set; }
			public string name { get; set; }
			public string banner_symbol_mesh_name { get; set; }
			public string banner_symbol_color { get; set; }
			public string banner_key { get; set; }
			public string occupation { get; set; }
			public string civilianTemplate { get; set; }
			public string battleTemplate { get; set; }
			public string is_companion { get; set; }
			public string offset { get; set; }
			public string level { get; set; }
			public string age { get; set; }
			public string is_mercenary { get; set; }
			public string formation_position_preference { get; set; }
			public string default_equipment_set { get; set; }
			public string skill_template { get; set; }
			public string upgrade_requires { get; set; }

			public string upgrade_targets_upgrade_target0_id { get; set; }
			public string upgrade_targets_upgrade_target1_id { get; set; }
			public string upgrade_targets_upgrade_target2_id { get; set; }
			public string upgrade_targets_upgrade_target3_id { get; set; }

			//Face - face_key
			public string Face_face_key_value { get; set; }
			public string Face_face_key_max_value { get; set; }
			public string Face_face_key_template_value { get; set; }
			//Face - BodyProperties
			public string Face_BodyProperties_version { get; set; }
			public string Face_BodyProperties_key { get; set; }
			public string Face_BodyProperties_age { get; set; }
			public string Face_BodyProperties_weight { get; set; }
			public string Face_BodyProperties_build { get; set; }
			//Face - BodyPropertiesMax
			public string Face_BodyPropertiesMax_version { get; set; }
			public string Face_BodyPropertiesMax_key { get; set; }
			public string Face_BodyPropertiesMax_age { get; set; }
			public string Face_BodyPropertiesMax_weight { get; set; }
			public string Face_BodyPropertiesMax_build { get; set; }
			//Face - hair_tags
			public string Face_hair_tags_hair_tag0_name { get; set; }
			public string Face_hair_tags_hair_tag1_name { get; set; }
			public string Face_hair_tags_hair_tag2_name { get; set; }
			//Face - beard_tags
			public string Face_beard_tags_beard_tag0_name { get; set; }
			public string Face_beard_tags_beard_tag1_name { get; set; }
			public string Face_beard_tags_beard_tag2_name { get; set; }
			//Face - tattoo_tags
			public string Face_tattoo_tags_tattoo_tag0_name { get; set; }
			public string Face_tattoo_tags_tattoo_tag1_name { get; set; }
			public string Face_tattoo_tags_tattoo_tag2_name { get; set; }
		}

		public static void Kingdoms_CSVtoXML(string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			KingdomRecord kingdomsRecord = new KingdomRecord();
			IEnumerable<KingdomRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Kingdoms");
				foreach (KingdomRecord record in records) {
					writer.WriteStartElement("Kingdom");

					//Changes

					//Defaults

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					if (!record.banner_key.Equals("")) writer.WriteAttributeString("banner_key", record.banner_key);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					writer.WriteAttributeString("primary_banner_color", record.primary_banner_color);
					writer.WriteAttributeString("secondary_banner_color", record.secondary_banner_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);
					writer.WriteAttributeString("name", "{=Kingdom." + record.id + ".name}" + record.name);
					if (!record.short_name.Equals("")) writer.WriteAttributeString("short_name", "{=Kingdom." + record.id + ".short_name}" + record.short_name);
					if (!record.text.Equals("")) writer.WriteAttributeString("text", "{=Kingdom." + record.id + ".text}" + record.text);
					if (!record.title.Equals("")) writer.WriteAttributeString("title", "{=Kingdom." + record.id + ".title}" + record.title);
					if (!record.ruler_title.Equals("")) writer.WriteAttributeString("ruler_title", "{=Kingdom." + record.id + ".ruler_title}" + record.ruler_title);

					if (!record.Policy0.Equals("") || !record.Policy1.Equals("") || !record.Policy2.Equals("") || !record.Policy3.Equals("")) {
						writer.WriteStartElement("policies");
						if (!record.Policy0.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy0);
							writer.WriteEndElement();
						}
						if (!record.Policy1.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy1);
							writer.WriteEndElement();
						}
						if (!record.Policy2.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy2);
							writer.WriteEndElement();
						}
						if (!record.Policy3.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy3);
							writer.WriteEndElement();
						}
						writer.WriteEndElement();
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class KingdomRecord {
			public string id { get; set; }
			public string banner_key { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string primary_banner_color { get; set; }
			public string secondary_banner_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
			public string title { get; set; }
			public string ruler_title { get; set; }

			public string Policy0 { get; set; }
			public string Policy1 { get; set; }
			public string Policy2 { get; set; }
			public string Policy3 { get; set; }

			public string Relationships_Kingdom0_name { get; set; }
			public string Relationships_Kingdom0_value { get; set; }
			public string Relationships_Kingdom0_isAtWar { get; set; }
			public string Relationships_Kingdom1_name { get; set; }
			public string Relationships_Kingdom1_value { get; set; }
			public string Relationships_Kingdom1_isAtWar { get; set; }
			public string Relationships_Kingdom2_name { get; set; }
			public string Relationships_Kingdom2_value { get; set; }
			public string Relationships_Kingdom2_isAtWar { get; set; }
			public string Relationships_Kingdom3_name { get; set; }
			public string Relationships_Kingdom3_value { get; set; }
			public string Relationships_Kingdom3_isAtWar { get; set; }

			public string Relationships_Clan0_name { get; set; }
			public string Relationships_Clan0_value { get; set; }
			public string Relationships_Clan0_isAtWar { get; set; }
			public string Relationships_Clan1_name { get; set; }
			public string Relationships_Clan1_value { get; set; }
			public string Relationships_Clan1_isAtWar { get; set; }
			public string Relationships_Clan2_name { get; set; }
			public string Relationships_Clan2_value { get; set; }
			public string Relationships_Clan2_isAtWar { get; set; }
			public string Relationships_Clan3_name { get; set; }
			public string Relationships_Clan3_value { get; set; }
			public string Relationships_Clan3_isAtWar { get; set; }

		}

		public static void Clans_CSVtoXML(string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			ClanRecord kingdomsRecord = new ClanRecord();
			IEnumerable<ClanRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Factions");
				foreach (ClanRecord record in records) {
					writer.WriteStartElement("Faction");

					//Changes
					record.is_bandit = record.is_bandit.ToLower();
					record.is_clan_type_mercenary = record.is_clan_type_mercenary.ToLower();
					record.is_mafia = record.is_mafia.ToLower();
					record.is_minor_faction = record.is_minor_faction.ToLower();
					record.is_nomad = record.is_nomad.ToLower();
					record.is_outlaw = record.is_outlaw.ToLower();
					record.is_sect = record.is_sect.ToLower();

					//Defaults
					if (record.tier.Equals("")) record.tier = "4";

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					writer.WriteAttributeString("tier", record.tier);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					if (!record.culture.Equals("")) writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.super_faction.Equals("")) writer.WriteAttributeString("super_faction", "Kingdom." + record.super_faction);
					if (!record.default_party_template.Equals("")) writer.WriteAttributeString("default_party_template", record.default_party_template);
					if (!record.is_bandit.Equals("")) writer.WriteAttributeString("is_bandit", record.is_bandit);
					if (!record.is_minor_faction.Equals("")) writer.WriteAttributeString("is_minor_faction", record.is_minor_faction);
					if (!record.is_outlaw.Equals("")) writer.WriteAttributeString("is_outlaw", record.is_outlaw);
					if (!record.is_clan_type_mercenary.Equals("")) writer.WriteAttributeString("is_clan_type_mercenary", record.is_clan_type_mercenary);
					if (!record.is_nomad.Equals("")) writer.WriteAttributeString("is_nomad", record.is_nomad);
					if (!record.is_sect.Equals("")) writer.WriteAttributeString("is_sect", record.is_sect);
					if (!record.is_mafia.Equals("")) writer.WriteAttributeString("is_mafia", record.is_mafia);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);
					writer.WriteAttributeString("name", "{=Clan." + record.id + ".name}" + record.name);
					if (!record.short_name.Equals("")) writer.WriteAttributeString("short_name", "{=Clan." + record.id + ".short_name}" + record.short_name);
					if (!record.text.Equals("")) writer.WriteAttributeString("text", "{=Clan." + record.id + ".text}" + record.text);

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class ClanRecord {
			public string id { get; set; }
			public string tier { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string super_faction { get; set; }
			public string default_party_template { get; set; }
			public string is_bandit { get; set; }
			public string is_minor_faction { get; set; }
			public string is_outlaw { get; set; }
			public string is_clan_type_mercenary { get; set; }
			public string is_nomad { get; set; }
			public string is_sect { get; set; }
			public string is_mafia { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
		}

		public static void Cultures_CSVtoXML (string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			CultureRecord cultureRecord = new CultureRecord();
			IEnumerable<CultureRecord> records = csv.EnumerateRecords(cultureRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("SPCultures");
				foreach (CultureRecord record in records) {
					writer.WriteStartElement("Culture");

					//Changes
					record.is_main_culture = record.is_main_culture.ToLower();
					record.is_bandit = record.is_bandit.ToLower();
					record.can_have_settlement = record.can_have_settlement.ToLower();

					//Defaults


					//Temporary


					//Write
					writer.WriteAttributeString("id",record.id);
					writer.WriteAttributeString("name", "{=Culture."+record.id+".name}"+record.name);
					if (!record.is_main_culture.Equals("")) writer.WriteAttributeString("is_main_culture", record.is_main_culture);
					if (!record.can_have_settlement.Equals("")) writer.WriteAttributeString("can_have_settlement", record.can_have_settlement);
					if (!record.town_edge_number.Equals("")) writer.WriteAttributeString("town_edge_number", record.town_edge_number);
					if (!record.militia_bonus.Equals("")) writer.WriteAttributeString("militia_bonus", record.militia_bonus);
					if (!record.prosperity_bonus.Equals("")) writer.WriteAttributeString("prosperity_bonus", record.prosperity_bonus);
					if (!record.encounter_background_mesh.Equals("")) writer.WriteAttributeString("encounter_background_mesh", record.encounter_background_mesh);
					writer.WriteAttributeString("basic_troop", "NPCCharacter." + record.basic_troop);
					writer.WriteAttributeString("elite_basic_troop", "NPCCharacter."+record.elite_basic_troop);

					//Temporary
					writer.WriteAttributeString("default_party_template", "PartyTemplate.kingdom_hero_party_empire_template");

					//Continue Write
					if (!record.male_names.Equals("")) {
						writer.WriteStartElement("male_names");

						string[] names = record.male_names.Split(";");
						
						foreach(string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name","{Culture.male_names." + name.ToLower() + "}"+name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.female_names.Equals("")) {
						writer.WriteStartElement("female_names");

						string[] names = record.female_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Culture.female_names." + name.ToLower() + "}" + name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.clan_names.Equals("")) {
						writer.WriteStartElement("clan_names");

						string[] names = record.clan_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Culture.clan_names." + name.ToLower() + "}" + name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class CultureRecord {
			public string id { get; set; }
			public string name { get; set; }
			public string is_main_culture { get; set; }
			public string can_have_settlement { get; set; }
			public string town_edge_number { get; set; }
			public string militia_bonus { get; set; }
			public string prosperity_bonus { get; set; }
			public string encounter_background_mesh { get; set; }
			public string basic_troop { get; set; }
			public string elite_basic_troop { get; set; }

			public string is_bandit { get; set; }
			public string default_face_key { get; set; }
			public string default_party_template { get; set; }
			public string villager_party_template { get; set; }
			public string elite_caravan_party_template { get; set; }
			public string bandit_boss_party_template { get; set; }
			public string caravan_party_template { get; set; }
			public string militia_party_template { get; set; }
			public string rebels_party_template { get; set; }

			public string male_names { get; set; }
			public string female_names { get; set; }
			public string clan_names { get; set; }
		}
	}
}
