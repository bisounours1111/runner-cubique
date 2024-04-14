using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class MapGen : MonoBehaviour
{
    public GameObject player;
    public GameObject[] Zone;
    public float Y;
    public float Z;

    public GameObject Obstacle;
    public GameObject Heart;

    public TextMeshProUGUI scoreText;

    public int hp = 3;

    public GameObject[] hearts;

    public GameObject gameOver;

    void Update()
    {

        Vector3 position = player.transform.position;

        scoreText.text = "Score : " + (position.z / 10).ToString("0");

        if (position.z > Z - 30)
        {
            GameObject objet = Zone[Random.Range(0, Zone.Length)];
            Z += 12f;
            if (objet.tag == "Ramp")
            {
                int random = Random.Range(0, 2);
                if (random == 1)
                {
                    Quaternion Angle = Quaternion.Euler(0, 0, 0);
                    GameObject insObjet = Instantiate(objet, new Vector3(0, Y, Z), Angle);
                    insObjet.transform.parent = transform;
                    insObjet.transform.name = "RampTop";
                    AssignTagRecursively(insObjet.transform, "RampTop");
                    Y += 4.5f;
                }
                else
                {
                    Y -= 4.5f;
                    Quaternion Angle = Quaternion.Euler(0, -180, 0);
                    GameObject insObjet = Instantiate(objet, new Vector3(0, Y, Z), Angle);
                    insObjet.transform.parent = transform;
                    insObjet.transform.name = "RampBot";
                    AssignTagRecursively(insObjet.transform, "RampBot");
                }
            }
            else
            {

                GameObject insObjet = Instantiate(objet, new Vector3(0, Y, Z), Quaternion.identity);
                insObjet.transform.name = "Platform";
                insObjet.transform.parent = transform;
                AssignTagRecursively(insObjet.transform, "Platform");

                Vector3 positionObstacle = insObjet.transform.position;

                for (int i = -1; i < 2; i += 2)
                {
                    int random = Random.Range(-1, 2);

                    for (int j = -1; j < 2; j++)
                    {
                        if (random != j)
                        {
                            if (hp < 3)
                            {
                                int randomX = Random.Range(0, 100);

                                if (randomX < 95)
                                {
                                    GameObject insObstacle = Instantiate(Obstacle, new Vector3(j * 1.5f, Y + Obstacle.transform.localScale.y / 2, positionObstacle.z + 3f * i), Quaternion.identity);
                                    insObstacle.transform.parent = insObjet.transform;
                                }
                                else
                                {
                                    GameObject insHeart = Instantiate(Heart, new Vector3(j * 1.5f, Y + Heart.transform.localScale.y / 2, positionObstacle.z + 3f * i),
                                        Quaternion.identity);

                                    insHeart.transform.parent = insObjet.transform;
                                }
                            }
                            else
                            {
                                GameObject insObstacle = Instantiate(Obstacle, new Vector3(j * 1.5f, Y + Obstacle.transform.localScale.y / 2, positionObstacle.z + 3f * i), Quaternion.identity);
                                insObstacle.transform.parent = insObjet.transform;
                            }
                        }
                    }
                }
            }

            DeletePlatform();
        }
    }

    public void DeletePlatform()
    {
        foreach (Transform child in transform)
        {
            if (child.position.z < player.transform.position.z - 10)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void AssignTagRecursively(Transform parent, string tag)
    {
        parent.gameObject.tag = tag;

        foreach (Transform child in parent)
        {
            if (child.parent.name != "Cell")
                AssignTagRecursively(child, tag);
        }
    }

    public void TakeDamage()
    {
        hp--;

        if (hp == 0)
        {
            GameOver();
        }


        RawImage heart = hearts[hp].GetComponent<RawImage>();
        heart.color = new Color(0, 0, 0, 255);
    }

    public void AddHp()
    {
        if (hp < 3)
        {
            hp++;
            RawImage heart = hearts[hp - 1].GetComponent<RawImage>();
            heart.color = new Color(255, 0, 0, 255);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
