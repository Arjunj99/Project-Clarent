﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    private float learningrate = 0.2f;
    public float LearningRate
    {
        get {return learningrate;}
        set {learningrate = value;}
    }
    int inputs;
    int hiddens;
    int outputs;
    public Matrix[] layers;
    Matrix[] biases;
    public NeuralNetwork(int numI, int numH, int numO)
    {
        inputs = numI;
        hiddens = numH;
        outputs = numO;
        layers = new Matrix[2];
        biases = new Matrix[2];
        layers[0] = new Matrix(hiddens, inputs);
        layers[1] = new Matrix(outputs, hiddens);
        biases[0] = new Matrix(hiddens, 1);
        biases[1] = new Matrix(outputs, 1);
        for (int l = 0; l < layers.Length; l++)
        {
            for (int i = 0; i < layers[l].Rows; i++)
            {
                for (int j = 0; j < layers[l].Columns; j++)
                {
                    layers[l][i, j] = Random.Range(-1f, 1f);
                    Random.InitState((int)System.DateTime.Now.Ticks);

                }
            }
        }
        for (int l = 0; l < biases.Length; l++)
        {
            for (int i = 0; i < biases[l].Rows; i++)
            {
                biases[l][i, 0] = Random.Range(-1f, 1f);
                Random.InitState((int)System.DateTime.Now.Ticks);

            }
        }
    }

    private Matrix WSum(Matrix input)
    {
        if (input.Rows == inputs)
        {
            return layers[0] * inputs;
        }
        else
        {
            throw new System.Exception("Wrong number of inputs");
        }
    }

    private float Sigmoid(float n)
    {
        return 1 / (1 + Mathf.Exp(-n));
    }
    private float DSigmoid(float n)
    {
        return n * (1 - n);
    }

    private Matrix deltaSigmoid(Matrix m)
    {
        Matrix retVal = new Matrix(m.Rows, m.Columns);
        for (int i = 0; i < m.Rows; i++)
        {
            for (int j = 0; j < m.Columns; j++)
            {
                retVal[i, j] = DSigmoid(m[i, j]);
            }
        }
        return retVal;
    }

    public float[] feedForward(float[] fInput)
    {
        Matrix input = new Matrix(fInput);
        for (int l = 0; l < layers.Length; l++)
        {
            Matrix hidden = layers[l] * input;
            hidden = hidden + biases[l];
            for (int i = 0; i < hidden.Rows; i++)
            {
                for (int j = 0; j < hidden.Columns; j++)
                {
                    hidden[i, j] = Sigmoid(hidden[i, j]);
                }
            }
            input = hidden;
        }
        return input.toArray();
    }

    public void Train(float[] fInput, float[] fCorrect)
    {
        Matrix input = new Matrix(fInput);
        Matrix[] rawOutputs = new Matrix[layers.Length];
        for (int l = 0; l < layers.Length; l++)
        {
            Matrix hidden = layers[l] * input;
            hidden = hidden + biases[l]; 
            for (int i = 0; i < hidden.Rows; i++)
            {
                for (int j = 0; j < hidden.Columns; j++)
                {
                    hidden[i, j] = Sigmoid(hidden[i, j]);
                }
            }
            rawOutputs[l] = hidden.duplicate();
            input = hidden;
        }
        Matrix error = new Matrix(outputs, 1);
        for (int i = 0; i < error.Rows; i++)
        {
            error[i, 0] = fCorrect[i] - rawOutputs[1].toArray()[i];
        }
        Matrix deltaB = learningrate * error.eWiseMultiply(deltaSigmoid(rawOutputs[1]));
        biases[1] += deltaB;
        deltaB *= rawOutputs[0].Transpose();
        layers[1] += deltaB;


        error = layers[1].Transpose() * error;
        deltaB = learningrate * error.eWiseMultiply(deltaSigmoid(rawOutputs[0]));
        biases[0] += deltaB;
        deltaB *= new Matrix(fInput).Transpose();
        layers[0] += deltaB;
    }
}
