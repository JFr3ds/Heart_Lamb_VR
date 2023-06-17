Current version:
3.0

Instructions:

Add Rect Transform Extended component to any UI GameObject to practice and better understand what these methods do.
Also 4 options are added to the Tools menu:
	- UI Anchors To Corners
	- UI Corners To Anchors
	- UI Center Rect
	- UI Center Anchors

The following methods are added to any RectTransform:

RectTransform.GetPosition()
RectTransform.SetPosition()

RectTransform.GetAnchorsPosition()
RectTransform.SetAnchorsPosition()

RectTransform.SetPositionX()
RectTransform.SetPositionY()

RectTransform.SetWidth()
RectTransform.SetHeight()

RectTransform.SetAnchorsPositionX()
RectTransform.SetAnchorsPositionY()

RectTransform.SetAnchorsWidth()
RectTransform.SetAnchorsHeight()

Usange example:

	Vector2 size = GetComponent<RectTransform>().GetSize(CoordinateSystem.IgnoreAnchorsAndPivot);

If you need more specific coordinate conversion use the methods of the static classes: RteRectTools and RteAnchorTools

For any question or issue:
fermmm@gmail.com