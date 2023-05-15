using UnityEngine; 


public class Back: MonoBehaviour
{
    public GameObject MainMenu; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            MainMenu.SetActive(true);
        }
    }


}
