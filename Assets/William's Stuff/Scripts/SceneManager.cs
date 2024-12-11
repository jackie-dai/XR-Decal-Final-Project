// Basically gets the object that holds the MRUK Room object,
// takes all of the prefabs and search for Table by name

using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> tables = new List<GameObject>();
    public List<MeshRenderer> tableRenderers = new List<MeshRenderer>();
    GameObject roomObject;
    bool lateStart = true;

    void Start()
    {
        OVRPassthroughLayer passthroughLayer;
        passthroughLayer = FindObjectOfType<OVRPassthroughLayer>();
        //passthroughLayer.enabled = false;
    }

    private void Update()
    {
        if (lateStart) {
            roomObject = FindObjectsOfType<MRUKRoom>()[0].gameObject;

            if (roomObject == null)
            {
                Debug.LogError("MRUKRoom not found");
                return;
            }

            foreach (Transform child in roomObject.transform)
            {
                if (child.gameObject.name == "TABLE")
                {
                    tables.Add(child.gameObject);
                    tableRenderers.Add(child.gameObject.GetComponentInChildren<MeshRenderer>());
                }
            }

            CreateTableBoxes();

            Destroy(roomObject);
            lateStart = false;
        }

    }

    public List<GameObject> GetTables() {
        return tables;
    }

    public void CreateTableBoxes()
    {
        foreach (MeshRenderer renderer in tableRenderers)
        {
            Bounds bounds = renderer.bounds;

            GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubeObject.name = "TABLE";

            cubeObject.transform.position = bounds.center;
            cubeObject.transform.rotation = renderer.transform.rotation;
            cubeObject.transform.Rotate(90, 0, 0);
            cubeObject.transform.localScale = bounds.size;

            MeshRenderer cubeRenderer = cubeObject.GetComponent<MeshRenderer>();
            Material whiteMaterial = new Material(Shader.Find("Standard"));
            whiteMaterial.color = Color.white;
            cubeRenderer.material = whiteMaterial;
            //cubeRenderer.enabled = false;
        }   
    }
}


















































//Don't use OVR Anchors, too complicated

//using System.Collections.Generic;
//using UnityEngine;

//public class SceneManager : MonoBehaviour 
//{
//    public List<OVRAnchor> anchors = new List<OVRAnchor>();

//    private async void Start()
//    {
//        Debug.Log("Checking Scene Manager");
//        // Create FetchOptions to get anchors with OVRSemanticLabels
//        var fetchOptions = new OVRAnchor.FetchOptions
//        {
//            SingleComponentType = typeof(OVRSemanticLabels) // Fetch only anchors with semantic labels
//        };

//        // Fetch anchors with semantic labels
//        var fetchResult = await OVRAnchor.FetchAnchorsAsync(anchors, fetchOptions);

//        Debug.Log("Did fetch exectue??? or still waiting");


//        // Check if fetch was successful
//        if (fetchResult.Status == OVRAnchor.FetchResult.Success && anchors.Count > 0)
//        {
//            Debug.Log($"Found {anchors.Count} anchors with semantic labels.");

//            // Filter for anchors labeled as "Table"
//            List<OVRAnchor> tableAnchors = new List<OVRAnchor>();
//            foreach (var anchor in anchors)
//            {
//                if (anchor.TryGetComponent(out OVRSemanticLabels semanticLabels))
//                {
//                    // Create a list to hold classifications
//                    List<OVRSemanticLabels.Classification> classifications = new List<OVRSemanticLabels.Classification>();
//                    semanticLabels.GetClassifications(classifications);

//                    // Check if the anchor has the "Table" classification
//                    if (classifications.Contains(OVRSemanticLabels.Classification.Table))
//                    {
//                        tableAnchors.Add(anchor);
//                    }
//                }
//            }

//            // Process the table anchors
//            foreach (var tableAnchor in tableAnchors)
//            {
//                ProcessTableAnchor(tableAnchor);
//            }
//        }
//        else
//        {
//            Debug.LogWarning("No anchors with semantic labels found or Fetch failed.");
//        }
//    }


//    private async void ProcessTableAnchor(OVRAnchor tableAnchor)
//    {
//        // Check if the anchor is locatable (has position and rotation)
//        if (tableAnchor.TryGetComponent(out OVRLocatable locatable))
//        {
//            // Enable the locatable component asynchronously
//            var enableResult = await locatable.SetEnabledAsync(true, 5.0); // 5-second timeout

//            // Check if enabling was successful
//            if (!enableResult)
//            {
//                Debug.LogWarning("Failed to enable the OVRLocatable component.");
//                return;
//            }

//            // Try to get the tracking space pose
//            if (locatable.TryGetSceneAnchorPose(out OVRLocatable.TrackingSpacePose trackingPose))
//            {
//                // Ensure we have a valid OVRCameraRig
//                OVRCameraRig cameraRig = FindObjectOfType<OVRCameraRig>();
//                if (cameraRig == null)
//                {
//                    Debug.LogError("OVRCameraRig not found in the scene. Ensure your OVRCameraRig prefab is set up.");
//                    return;
//                }

//                // Use OVRCameraRig's tracking space transform to convert position and rotation
//                Transform trackingSpaceToWorld = cameraRig.trackingSpace; // OVRCameraRig's tracking space transform
//                Vector3? worldPosition = trackingPose.ComputeWorldPosition(trackingSpaceToWorld);
//                Quaternion? worldRotation = trackingPose.ComputeWorldRotation(trackingSpaceToWorld);

//                // Ensure position and rotation are valid
//                if (worldPosition.HasValue && worldRotation.HasValue)
//                {
//                    // Check if the table anchor has a 3D mesh
//                    if (tableAnchor.TryGetComponent(out OVRTriangleMesh triangleMesh))
//                    {
//                        // Fetch the mesh data
//                        Mesh tableMesh = new Mesh();

//                        // Create a new GameObject to represent the table
//                        GameObject tableObject = new GameObject("Table");

//                        // Set the position and rotation of the table GameObject to match the anchor
//                        tableObject.transform.position = worldPosition.Value;
//                        tableObject.transform.rotation = worldRotation.Value;

//                        // Add a MeshFilter to visualize the mesh
//                        MeshFilter meshFilter = tableObject.AddComponent<MeshFilter>();
//                        meshFilter.mesh = tableMesh;

//                        // Add a MeshRenderer for visualization
//                        MeshRenderer meshRenderer = tableObject.AddComponent<MeshRenderer>();
//                        Material whiteMaterial = new Material(Shader.Find("Standard"));
//                        whiteMaterial.color = Color.white; // Set color to white
//                        meshRenderer.material = whiteMaterial;

//                        // Add a MeshCollider for physical interactions
//                        MeshCollider meshCollider = tableObject.AddComponent<MeshCollider>();
//                        meshCollider.sharedMesh = tableMesh;

//                        Debug.Log("Created a table with a 3D mesh representation.");


//                    }
//                    else
//                    {
//                        Debug.LogWarning("The table anchor does not have a 3D mesh component.");
//                    }
//                }
//                else
//                {
//                    Debug.LogWarning("World position or rotation is invalid.");
//                }
//            }
//            else
//            {
//                Debug.LogWarning("Unable to get TrackingSpacePose for the table anchor.");
//            }
//        }
//        else
//        {
//            Debug.LogWarning("The table anchor is not locatable.");
//        }
//    }
//}









///*
//_____________________________________________________________________________________
//______________________________________________________________________________________
// */

//// LIKE GPT SO DUMB I HAVE TO WRITE IT MYSELF U STUPID FUCK
////using System.Collections.Generic;
////using UnityEngine;
////using Oculus.Interaction;

////public class CustomSceneManager : MonoBehaviour
////{
////    private async void Start()
////    {
////        // Fetch room anchors by component
////        List<OVRAnchor> roomAnchors = new List<OVRAnchor>();
////        var fetchResult = await OVRAnchor.FetchAnchorsAsync(roomAnchors, new OVRAnchor.FetchOptions
////        {
////            SingleComponentType = typeof(OVRRoomLayout) // Fetch anchors with RoomLayout component
////        });

////        // Check if room anchors were fetched successfully
////        if (fetchResult != null && roomAnchors.Count > 0)
////        {
////            Debug.Log($"Found {roomAnchors.Count} room anchors.");

////            // Iterate through room anchors and fetch child anchors
////            foreach (var roomAnchor in roomAnchors)
////            {
////                Debug.Log("Room Anchor Found."); // Use logging to track the anchors
////                await FetchChildAnchors(roomAnchor);
////            }
////        }
////        else
////        {
////            Debug.LogWarning("No room anchors found or Fetch failed.");
////        }
////    }

////    private async System.Threading.Tasks.Task FetchChildAnchors(OVRAnchor parentAnchor)
////    {
////        // Fetch all anchors and manually filter for children of the specified parent anchor
////        List<OVRAnchor> allAnchors = new List<OVRAnchor>();
////        var fetchResult = await OVRAnchor.FetchAnchorsAsync(allAnchors, new OVRAnchor.FetchOptions());

////        if (fetchResult != null && allAnchors.Count > 0)
////        {
////            // Filter child anchors by spatial proximity or other logic since `ParentAnchor` isn't available
////            var childAnchors = new List<OVRAnchor>();
////            foreach (var anchor in allAnchors)
////            {
////                if (IsChildOfParent(anchor, parentAnchor))
////                {
////                    childAnchors.Add(anchor);
////                }
////            }

////            Debug.Log($"Found {childAnchors.Count} child anchors for the specified parent.");

////            // Process each child anchor
////            foreach (var childAnchor in childAnchors)
////            {
////                ProcessChildAnchor(childAnchor);
////            }
////        }
////        else
////        {
////            Debug.LogWarning("No child anchors found or Fetch failed.");
////        }
////    }

////    private bool IsChildOfParent(OVRAnchor child, OVRAnchor parent)
////    {
////        // Example logic to determine if one anchor is a child of another
////        // You can implement additional checks like proximity, hierarchy, etc.
////        return Vector3.Distance(child.transform.position, parent.transform.position) < 1.0f;
////    }

////    private void ProcessChildAnchor(OVRAnchor childAnchor)
////    {
////        // Example: Check if the child anchor is classified as a table
////        if (childAnchor.TryGetComponent(out OVRSemanticLabels labels))
////        {
////            foreach (string label in labels.Labels)
////            {
////                if (label.Equals(OVRSceneManager.Classification.Table.ToString()))
////                {
////                    Debug.Log("Found a table anchor.");

////                    // Optional: Further processing like enabling tracking or creating visualizations
////                }
////            }
////        }
////    }
////}
