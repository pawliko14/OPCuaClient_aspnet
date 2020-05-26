using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Opc.Ua;
using Opc.Ua.Client;
using Siemens.UAClientHelper;

namespace Siemens.OpcUA.SimpleClient
{
    public partial class MainForm : Form
    {
        #region Construction
        public MainForm()
        {
            InitializeComponent();

            m_Server = new UAClientHelperAPI();
            m_Server.CertificateValidationNotification += new CertificateValidationEventHandler(m_Server_CertificateEvent);
        }

        void m_Server_CertificateEvent(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            // Accept all certificate -> better ask user
            e.Accept = true;
        }
        #endregion

        #region Fields
        private UAClientHelperAPI m_Server = null;
        private Subscription m_Subscription;
        private Subscription m_SubscriptionBlock;
        private UInt16 m_NameSpaceIndex = 0;

       
        #endregion

        /// <summary>
        /// Connect to the UA server and read the namespace table.
        /// The connect is based on the Server URL entered in the Form
        /// The read of the namespace table is used to detect the namespace index
        /// of the namespace URI entered in the Form and used for the variables to read
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Connect to the server
            try
            {
                // Connect with URL from Server URL text box
                m_Server.Connect(txtServerUrl.Text, "none", MessageSecurityMode.SignAndEncrypt, true, "OpcUaClient", "SUNRISE");

                  

                // Toggle enable flag of buttons
                toggleButtons(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect failed:\n\n" + ex.Message);
                return;
            }

            // Read Namespace Table
            try
            {
                List<string> nodesToRead = new List<string>();
                List<string> results = new List<string>();

                nodesToRead.Add("ns=0;i=" + Variables.Server_NamespaceArray.ToString());

                // Read the namespace array
                results = m_Server.ReadValues(nodesToRead);

                if (results.Count != 1)
                {
                    throw new Exception("Reading namespace table returned unexptected result");
                }

                // Try to find the namespace URI entered by the user
                string[] nameSpaceArray = results[0].Split(';');
                ushort i;
                for (i = 0; i < nameSpaceArray.Length; i++)
                {
                    if (nameSpaceArray[i] == txtNamespaceUri.Text)
                    {
                        m_NameSpaceIndex = i;
                    }
                }

                // Check if the namespace was found
                if ( m_NameSpaceIndex == 0 )
                {
                    throw new Exception("Namespace " + txtNamespaceUri.Text + " not found in server namespace table");
                }
                else
                {
                    throw new Exception("Success to read namespace");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reading namespace table failed:\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Disconnect from the UA server.
        /// </summary>
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_Subscription != null)
                {
                    btnMonitor_Click(null, null);
                }

                if (m_SubscriptionBlock != null)
                {
                    btnMonitorBlock_Click(null, null);
                }

                // Disconnect from Server
                m_Server.Disconnect();

                // Toggle enable flag of buttons
                toggleButtons(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconnect failed:\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Reads the values of the two variables entered in the From.
        /// The NodeIds used for the Read are constructed from the identifier entered 
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> nodesToRead = new List<string>();
                List<string> results = new List<string>();

                // Add the two variable NodeIds to the list of nodes to read
                // NodeId is constructed from 
                // - the identifier text in the text box
                // - the namespace index collected during the server connect
                nodesToRead.Add(new NodeId(txtIdentifier1.Text, m_NameSpaceIndex).ToString());
                nodesToRead.Add(new NodeId(txtIdentifier2.Text, m_NameSpaceIndex).ToString());

                Console.WriteLine("-------------------------");
                Console.WriteLine("-------------------------");
                Console.WriteLine("-------------------------");
                Console.WriteLine("--NAMESPACEINDEX: -" + m_NameSpaceIndex);
                Console.WriteLine("-------------------------");
                Console.WriteLine("--Textident: --" + txtIdentifier1.Text);
                Console.WriteLine("-------------------------");
                Console.WriteLine("-------------------------");


                // Read the values
                results = m_Server.ReadValues(nodesToRead);

                txtRead1.Text = results[0];
                txtRead2.Text = results[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read failed:\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Starts the monitoring of the values of the two variables entered in the From.
        /// The NodeIds used for the monitoring are constructed from the identifier entered 
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void btnMonitor_Click(object sender, EventArgs e)
        {
            // Check if we have a subscription 
            //  - No  -> Create a new subscription and create monitored items
            //  - Yes -> Delete Subcription
            if (m_Subscription == null)
            {
                try
                {
                    // Create subscription
                    m_Subscription = m_Server.Subscribe(1000);
                    m_Server.ItemChangedNotification += new MonitoredItemNotificationEventHandler(ClientApi_ValueChanged);
                    btnMonitor.Text = "Stop";

                    // Create first monitored item
                    m_Server.AddMonitoredItem(m_Subscription, new NodeId(txtIdentifier1.Text, m_NameSpaceIndex).ToString(), "item1", 100);


                    // Create second monitored item
                    m_Server.AddMonitoredItem(m_Subscription, new NodeId(txtIdentifier2.Text, m_NameSpaceIndex).ToString(), "item2", 100);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Establishing data monitoring failed:\n\n" + ex.Message);
                }
            }
            else
            {
                try
                {
                    m_Server.RemoveSubscription(m_Subscription);
                    m_Subscription = null;

                    btnMonitor.Text = "Monitor";
                    txtMonitored1.Text = "";
                    txtMonitored2.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Stopping data monitoring failed:\n\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Callback method for data changes from the monitored variables.
        /// The text boxes for the output of the values or status information are passed 
        /// to the client API as clientHandles and contained in the callback
        /// </summary>
        private void ClientApi_ValueChanged(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MonitoredItemNotificationEventHandler(ClientApi_ValueChanged), monitoredItem, e);
                    return;
                }
                MonitoredItemNotification notification = e.NotificationValue as MonitoredItemNotification;
                if (notification == null)
                {
                    return;
                }

                if (monitoredItem.DisplayName == "item1")
                {
                    // Get the according item
                    txtMonitored1.Text = notification.Value.WrappedValue.ToString();
                }

                if (monitoredItem.DisplayName == "item2")
                {
                    // Get the according item
                    txtMonitored2.Text = notification.Value.WrappedValue.ToString();
                }

                if (monitoredItem.DisplayName == "itemBlock1")
                {
                    // Get the according item
                    txtReadBlock.Text = notification.Value.WrappedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error in the data change callback:\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Write the value of the first variable entered in the From.
        /// The NodeId used for the Write is constructed from the identifier entered 
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void btnWrite1_Click(object sender, EventArgs e)
        {
            writeNewValue(
                new NodeId(txtIdentifier1.Text, m_NameSpaceIndex),  // NodeId = identifier + namespace index
                txtWrite1.Text); // Value to write as string
        }

        /// <summary>
        /// Write the value of the second variable entered in the From.
        /// The NodeId used for the Write is constructed from the identifier entered 
        /// in the Form and the namespace index detected in the connect method
        /// </summary>
        private void btnWrite2_Click(object sender, EventArgs e)
        {
            writeNewValue(
                new NodeId(txtIdentifier2.Text, m_NameSpaceIndex), // NodeId = identifier + namespace index
                txtWrite2.Text); // Value to write as string
        }

        /// <summary>
        /// Helper function to writing a value to a variable.
        /// The function 
        /// - reads the data type of the variable
        /// - converts the passed string to the data type
        /// - writes the value to the variable
        /// </summary>
        private void writeNewValue(NodeId nodeToWrite, string valueToWrite)
        {
            try
            {
                List<string> nodesToWrite = new List<string>();
                List<string> values = new List<string>();

                nodesToWrite.Add(nodeToWrite.ToString());
                values.Add(valueToWrite);

                m_Server.WriteValues(values, nodesToWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Writing new value failed:\n\n" + ex.Message);
            }
        }

        private void btnMonitorBlock_Click(object sender, EventArgs e)
        {
            // Check if we have a subscription 
            //  - No  -> Create a new subscription and create monitored item
            //  - Yes -> Delete Subcription
            if (m_SubscriptionBlock == null)
            {
                try
                {
                    // Create subscription
                    m_SubscriptionBlock = m_Server.Subscribe(1000);
                    btnMonitorBlock.Text = "Stop";

                    // Create first monitored item
                    m_Server.AddMonitoredItem(m_SubscriptionBlock, new NodeId(txtIdentifierBlockRead.Text, m_NameSpaceIndex).ToString(), "itemBlock1", 100);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Establishing block monitoring failed:\n\n" + ex.Message);
                }
            }
            else
            {
                try
                {
                    m_Server.RemoveSubscription(m_SubscriptionBlock);
                    m_SubscriptionBlock = null;

                    btnMonitorBlock.Text = "Monitor Block";
                    txtReadBlock.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Stopping block monitoring failed:\n\n" + ex.Message);
                }
            }
        }

        private void btnWriteBlock1_Click(object sender, EventArgs e)
        {
            int writeLength = (int)Convert.ChangeType(txtWriteLength.Text, typeof(int));
            byte[] rawValue = new byte[writeLength];
            byte currentValue = 0;
            object writeValue;

            for (int i = 0; i < rawValue.Count(); i++)
            {
                rawValue[i] = currentValue;
                currentValue++;
            }

            writeValue = rawValue;

            writeNewBlockValue(
                new NodeId(txtIdentifierBlockWrite.Text, m_NameSpaceIndex), // NodeId = identifier + namespace index
                writeValue); // Value to write as byte array
        }

        private void btnWriteBlock2_Click(object sender, EventArgs e)
        {
            int writeLength = (int)Convert.ChangeType(txtWriteLength.Text, typeof(int));
            byte[] rawValue = new byte[writeLength];
            byte currentValue = 255;
            object writeValue;

            for (int i = 0; i < rawValue.Count(); i++)
            {
                rawValue[i] = currentValue;
                currentValue--;
            }

            writeValue = rawValue;

            writeNewBlockValue(
                new NodeId(txtIdentifierBlockWrite.Text, m_NameSpaceIndex), // NodeId = identifier + namespace index
                writeValue); // Value to write as byte array
        }

        /// <summary>
        /// Helper function to writing a value to a variable.
        /// The function 
        /// - reads the data type of the variable
        /// - converts the passed string to the data type
        /// - writes the value to the variable
        /// </summary>
        private void writeNewBlockValue(NodeId nodeToWrite, object valueToWrite)
        {
            try
            {
                List<string> nodesToWrite = new List<string>();
                List<string> values = new List<string>();

                nodesToWrite.Add(nodeToWrite.ToString());
                values.Add(valueToWrite.ToString());

                m_Server.WriteValues(values, nodesToWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Writing new block value failed:\n\n" + ex.Message);
            }
        }

        private void toggleButtons(bool isConnected)
        {
            // Toggle Connect / Disconnect buttons
            btnConnect.Enabled = !isConnected;
            btnDisconnect.Enabled = isConnected;

            // Toggle Textboxes
            txtServerUrl.Enabled = !isConnected;
            txtNamespaceUri.Enabled = !isConnected;

            // Toggle action buttons
            btnMonitor.Enabled = isConnected;
            btnRead.Enabled = isConnected;
            btnWrite1.Enabled = isConnected;
            btnWrite2.Enabled = isConnected;
            btnMonitorBlock.Enabled = isConnected;
            btnWriteBlock1.Enabled = isConnected;
            btnWriteBlock2.Enabled = isConnected;
        }

        private void txtMonitored1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
