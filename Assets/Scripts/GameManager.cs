using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unasweredQuestion;

    private Question currentQuestion;
    public Text scoreText;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;


    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    void Start()
    {
        // Checks if first screen or all questions have been answered. Will add all questions to question List.
        // only Executes if first load OR in all questions have been answered
        if (unasweredQuestion == null || unasweredQuestion.Count == 0) {
            unasweredQuestion = questions.ToList<Question>();
        }


        SetCurrentQuestion();
        
    }

    void SetCurrentQuestion() {
        // gets random question from the list and removes it from the list so it can not be chosen again.
        int randomQuestionIndex = Random.Range (0, unasweredQuestion.Count);
        currentQuestion = unasweredQuestion[randomQuestionIndex];

        factText.text = currentQuestion.fact;
        scoreText.text = "SCORE: " + score.totalScore.ToString();

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion () {
        // removes the question at index, prevents picking same question twice.
        unasweredQuestion.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue () {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue) {
            score.totalScore += 1;
            scoreText.text = "SCORE: " + score.totalScore.ToString();
        }
        else {
            
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse() {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue){
            score.totalScore += 1;
            scoreText.text = "SCORE: " + score.totalScore.ToString();
        }
        else {
            
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    
}
