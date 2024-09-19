using UnityEngine;

public class TiltWindow1 : MonoBehaviour
{
	public Vector2 range = new Vector2(5f, 3f);
	public float min = -1;
	public float max = 1;
	Transform mTrans;
	Quaternion mStart;
	Vector2 mRot = Vector2.zero;

	void Start ()
	{
		mTrans = transform;
		mStart = mTrans.localRotation;
	}

	void Update ()
	{
		
		Vector3 pos = Input.mousePosition;

		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, max, min);
		//replaced clamp confines with variables we can edit in the editor inorder to customise the tilting range and angle to better fit our game
		float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
		mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);

		mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * -range.x, 0f);
	}
}
