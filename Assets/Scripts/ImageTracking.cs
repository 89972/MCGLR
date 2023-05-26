using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    public string ReferenceImageName;
    private ARTrackedImageManager _TrackedImageManager;

    private void Awake()
    {
        _TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        if(_TrackedImageManager != null)
        {
            _TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    void OnDisabled()
    {
        if(_TrackedImageManager != null)
        {
            _TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }
    
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs e)
    {
        foreach (var trackedImage in e.added)
        {
            Debug.Log($"Tracked image detected {trackedImage.ReferenceImageName.name} with size {trackedImage.size}");
        }

        UpdateTrackedImages(e.added);
        UpdateTrackedImages(e.updated);
    }

        private void UpdateTrackedImages(IEnumerable<ARTrackedImage> trackedImages)
    {
        // If the same image (RefrenceImageName)
         var trackedImage = trackedImage.FirstOrDefault(x => x.referenceImageName == ReferenceImageName); 
         if (trackedImage == null)
         {
            return;
         }
      

    if (trackedImage.trackingState != trackingState.None)
    {
        var trackedImageTransform = trackedImage.transorm;
        transform.SetPositionAndRotation(trackedImageTransform.position, trackedImageTransform.rotation);
    }
    
   
}
}