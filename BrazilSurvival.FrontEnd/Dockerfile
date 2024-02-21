FROM node:18-alpine as build
WORKDIR /app
COPY package.json .
RUN apk update
RUN apk upgrade
RUN apk add curl

RUN npm install
COPY . .
RUN npm run build

FROM node:18-alpine as production
WORKDIR /app

COPY --from=build /app/dist /app/dist
EXPOSE 8080

COPY package.json .
COPY vite.config.ts .

RUN npm install typescript
CMD ["npm", "run", "preview"]